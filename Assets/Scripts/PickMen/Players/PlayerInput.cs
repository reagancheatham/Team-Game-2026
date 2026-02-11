using Shears.Input;
using UnityEngine;

namespace PickMen.Players
{
    [DefaultExecutionOrder(-100)]
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        private ManagedInputMap inputMap;

        public ManagedInputMap InputMap => inputMap;
        public IManagedInput MoveInput { get; private set; }
        public IManagedInput SprintInput { get; private set; }
        public IManagedInput JumpInput { get; private set; }
        public IManagedInput CrouchInput { get; private set; }
        public IManagedInput LookInput { get; private set; }
        public IManagedInput InteractInput { get; private set; }
        public IManagedInput DropInput { get; private set; }

        private void Awake()
        {
            inputMap.GetInputs(
                ("Move", i => MoveInput = i),
                ("Sprint", i => SprintInput = i),
                ("Jump", i => JumpInput = i),
                ("Crouch", i => CrouchInput = i),
                ("Look", i => LookInput = i),
                ("Interact", i => InteractInput = i),
                ("Drop", i => DropInput = i)
            );
        }
    }
}
