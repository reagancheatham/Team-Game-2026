using Shears;
using Shears.Detection;
using Shears.Input;
using System.Collections;
using UnityEngine;

namespace PickMen.Players
{
    public partial class Player : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private PlayerInput input;

        [SerializeField]
        private Transform relativeTransform;

        [SerializeField]
        private CharacterController controller;

        [SerializeField]
        private AreaDetector3D groundDetector;

        [Header("Settings")]
        [SerializeField]
        private float moveSpeed = 5.0f;

        [SerializeField]
        private float jumpPower = 4.0f;

        [AutoEvent(nameof(IManagedInput.Performed), nameof(OnJumpInput))]
        private IManagedInput jumpInput;
        private IManagedInput moveInput;

        private readonly Timer jumpTimer = new(0.1f);
        private bool isGrounded;

        private void Awake()
        {
            moveInput = input.MoveInput;
            jumpInput = input.JumpInput;

            CursorManager.SetCursorVisibility(false);
            CursorManager.SetCursorLockMode(CursorLockMode.Locked);
        }

        private void Update()
        {
            UpdateIsGrounded();
            UpdateMovement();
        }

        private void UpdateIsGrounded()
        {
            isGrounded = groundDetector.Detect();
        }

        private void UpdateMovement()
        {
            Vector2 input = moveInput.ReadValue<Vector2>();
            Vector3 forward = relativeTransform.forward * input.y;
            Vector3 right = relativeTransform.right * input.x;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            Vector3 movement = moveSpeed * Time.deltaTime * (forward + right).normalized;

            controller.Move(movement);
        }

        private void OnJumpInput()
        {
            if (!isGrounded)
                return;

            StartCoroutine(IEJump());
        }

        private IEnumerator IEJump()
        {
            float currentPower = jumpPower;
            jumpTimer.Start();

            while (!jumpTimer.IsDone || !isGrounded)
            {
                controller.Move(Time.deltaTime * new Vector3(0, currentPower, 0));

                currentPower += Time.deltaTime * Physics.gravity.y;

                yield return null;
            }
        }
    }
}
