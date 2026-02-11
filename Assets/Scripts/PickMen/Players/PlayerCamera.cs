using Shears;
using Shears.Cameras;
using UnityEngine;

namespace PickMen.Players
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput input;

        [SerializeField]
        private CharacterController controller;

        [SerializeField]
        new private ManagedCamera camera;

        [SerializeField]
        private FirstPersonCameraState cameraState;

        private void Awake()
        {
            camera.Input = input.InputMap;

            camera.Initialize();
        }

        private void Update()
        {
            cameraState.Offset = cameraState.Offset.With(y: 0.75f * controller.height);
        }
    }
}
