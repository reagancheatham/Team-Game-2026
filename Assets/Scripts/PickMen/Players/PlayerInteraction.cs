using PickMen.Interaction;
using PickMen.Levels;
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
        private PlayerMovement playerMovement;

        [SerializeField]
        private PlayerInventory inventory;

        [SerializeField]
        private RayDetector3D detector;

        [AutoEvent(nameof(IManagedInput.Performed), nameof(TryInteract))]
        private IManagedInput interactInput;

        private void Awake()
        {
            interactInput = input.InteractInput;
        }

        private void TryInteract()
        {
            if (!detector.Detect())
                return;

            if (detector.TryGetDetection(out Item item, true))
                inventory.AddItem(item);
            else if (detector.TryGetDetection(out Teleporter teleporter, true))
            {
                playerMovement.Controller.enabled = false;
                playerMovement.transform.SetPositionAndRotation(
                    teleporter.TargetPosition.position, 
                    teleporter.TargetPosition.rotation
                );
                playerMovement.Controller.enabled = true;
            }
        }
    }
}
