using PickMen.Interaction;
using Shears;
using Shears.Detection;
using Shears.Input;
using UnityEngine;

namespace PickMen.Players
{
    public partial class PlayerInteraction : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput input;

        [SerializeField]
        private RayDetector3D detector;

        [SerializeField]
        private Transform hand;

        [AutoEvent(nameof(IManagedInput.Performed), nameof(OnInteractInput))]
        private IManagedInput interactInput;

        private void Awake()
        {
            interactInput = input.InteractInput;
        }

        private void OnInteractInput()
        {
            if (!detector.Detect())
                return;

            if (detector.TryGetDetection(out Pickup pickup))
            {
                PickUp(pickup);
                return;
            }
        }

        private void PickUp(Pickup pickup)
        {
            pickup.transform.SetParent(hand);
            pickup.transform.localPosition = Vector3.zero;
        }
    }
}
