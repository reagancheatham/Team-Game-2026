using Shears;
using UnityEngine;

namespace PickMen.Interaction.Graphics
{
    [RequireComponent(typeof(Pickup))]
    public partial class PickupGraphics : MonoBehaviour
    {
        private const string INTERACTABLE_LAYER = "Interactable";
        private const string FIRST_PERSON_LAYER = "First Person";

        [SerializeField]
        new private Renderer renderer;

        [Auto]
        [AutoEvent(nameof(Pickup.PickedUp), nameof(OnPickedUp))]
        [AutoEvent(nameof(Pickup.Released), nameof(OnReleased))]
        private Pickup pickup;

        private void OnPickedUp()
        {
            renderer.gameObject.layer = LayerMask.NameToLayer(FIRST_PERSON_LAYER);
        }

        private void OnReleased()
        {
            renderer.gameObject.layer = LayerMask.NameToLayer(INTERACTABLE_LAYER);
        }
    }
}
