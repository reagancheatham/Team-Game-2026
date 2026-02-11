using Shears;
using Shears.Detection;
using Shears.Input;
using Shears.Tweens;
using System.Collections;
using UnityEngine;

namespace PickMen.Players
{
    public class PlayerMovement : MonoBehaviour
    {
        private static readonly TweenData crouchTweenData = new(0.2f, easingFunction: TweenEase.OutQuad);

        [SerializeField]
        private Transform relativeTransform;

        [SerializeField]
        private CharacterController controller;

        [SerializeField]
        private AreaDetector3D groundDetector;

        private readonly Timer jumpTimer = new(0.1f);
        private Tween crouchTween;
        private PlayerData data;
        private PlayerInput input;
        private bool isGrounded;
        private bool isCrouching;
        private bool isListeningToJump;
        private float originalHeight;

        public void Initialize(PlayerData data, PlayerInput input)
        {
            this.data = data;
            this.input = input;
            originalHeight = controller.height;

            input.JumpInput.Performed += OnJumpInput;
            isListeningToJump = true;
        }

        private void OnEnable()
        {
            if (input == null || isListeningToJump)
                return;

            input.JumpInput.Performed += OnJumpInput;
        }

        private void OnDisable()
        {
            if (input == null)
                return;

            input.JumpInput.Performed -= OnJumpInput;
            isListeningToJump = false;
        }

        public void UpdateMovement()
        {
            UpdateIsCrouched();
            UpdateIsGrounded();
            ApplyMovement();
        }

        private void UpdateIsGrounded()
        {
            isGrounded = groundDetector.Detect();
        }

        private void UpdateIsCrouched()
        {
            float currentHeight = controller.height;
            float currentCenterY = controller.center.y;

            if (!input.CrouchInput.IsPressed() || !isGrounded)
            {
                if (isCrouching)
                {
                    crouchTween.Dispose();
                    crouchTween = TweenManager.DoTween(t =>
                    {
                        controller.height = Mathf.LerpUnclamped(currentHeight, originalHeight, t);
                        controller.center = Vector3.LerpUnclamped(
                            controller.center.With(y: currentCenterY),
                            controller.center.With(y: 0.5f * originalHeight),
                            t);
                    }, crouchTweenData).WithLifetime(this);

                    isCrouching = false;
                }

                return;
            }

            // else, i am grounded and pressing the button
            if (isCrouching)
                return;

            crouchTween.Dispose();
            crouchTween = TweenManager.DoTween(t =>
            {
                controller.height = Mathf.LerpUnclamped(currentHeight, data.CrouchHeight, t);
                controller.center = Vector3.LerpUnclamped(
                    controller.center.With(y: currentCenterY),
                    controller.center.With(y: 0.5f * data.CrouchHeight),
                    t);
            }, crouchTweenData).WithLifetime(this);

            isCrouching = true;
        }

        private void ApplyMovement()
        {
            Vector2 input = this.input.MoveInput.ReadValue<Vector2>();
            Vector3 forward = relativeTransform.forward * input.y;
            Vector3 right = relativeTransform.right * input.x;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            Vector3 movement = GetMoveSpeed() * Time.deltaTime * (forward + right).normalized;

            controller.Move(movement);
        }

        private void OnJumpInput()
        {
            if (!isGrounded || isCrouching)
                return;

            StartCoroutine(IEJump());
        }

        private IEnumerator IEJump()
        {
            float currentPower = data.JumpPower;
            jumpTimer.Start();

            while (!jumpTimer.IsDone || !isGrounded)
            {
                controller.Move(Time.deltaTime * new Vector3(0, currentPower, 0));

                currentPower += Time.deltaTime * Physics.gravity.y;

                yield return null;
            }
        }

        private float GetMoveSpeed()
        {
            if (isCrouching)
                return data.CrouchSpeed;
            else if (!isGrounded)
                return data.AirSpeed;
            else if (input.SprintInput.IsPressed())
                return data.SprintSpeed;
            else
                return data.WalkSpeed;
        }
    }
}
