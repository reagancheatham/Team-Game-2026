using Shears;
using UnityEngine;

namespace PickMen.Interaction.Graphics
{
    [RequireComponent(typeof(Item))]
    public partial class ItemGraphics : MonoBehaviour
    {
        private const string INTERACTABLE_LAYER = "Interactable";
        private const string FIRST_PERSON_LAYER = "First Person";

        [SerializeField]
        private GameObject model;

        [Auto]
        [AutoEvent(nameof(Item.PickedUp), nameof(OnPickedUp))]
        [AutoEvent(nameof(Item.Released), nameof(OnReleased))]
        private Item pickup;

        private void OnPickedUp()
        {
            var layer = LayerMask.NameToLayer(FIRST_PERSON_LAYER);

            model.layer = layer;
            model.SetLayerOnAllChildren(layer);
        }

        private void OnReleased()
        {
            var layer = LayerMask.NameToLayer(INTERACTABLE_LAYER);

            model.layer = layer;
            model.SetLayerOnAllChildren(layer);
        }
    }
}
