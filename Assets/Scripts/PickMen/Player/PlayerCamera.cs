using Shears.Cameras;
using UnityEngine;

namespace PickMen.Players
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput input;

        [SerializeField]
        new private ManagedCamera camera;

        private void Awake()
        {
            camera.Input = input.InputMap;
        }
    }
}
