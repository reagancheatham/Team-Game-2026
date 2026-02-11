using PickMen.Interaction;
using Shears;
using Shears.Detection;
using Shears.Input;
using UnityEngine;

namespace PickMen.Players
{
    public partial class PlayerInteraction : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private PlayerInput input;

        [SerializeField]
        private PlayerItemHolder itemHolder;

        [SerializeField]
        private RayDetector3D detector;

        [AutoEvent(nameof(IManagedInput.Performed), nameof(OnInteractInput))]
        private IManagedInput interactInput;

        [AutoEvent(nameof(IManagedInput.Performed), nameof(OnDropInput))]
        private IManagedInput dropInput;

        private void Awake()
        {
            interactInput = input.InteractInput;
            dropInput = input.DropInput;
        }

        private void OnInteractInput()
        {
            if (itemHolder.HeldItem != null)
                return;
            else
                TryInteract();
        }

        private void OnDropInput()
        {
            if (itemHolder.HeldItem != null)
                itemHolder.Release();
        }

        private void TryInteract()
        {
            if (!detector.Detect())
                return;

            if (detector.TryGetDetection(out Pickup pickup, true))
            {
                itemHolder.PickUp(pickup);
                return;
            }
        }
    }
}
