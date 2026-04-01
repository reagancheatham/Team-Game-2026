using PickMen.Interaction;
using Shears;
using UnityEngine;
using UnityEngine.UI;

namespace PickMen.Players.Graphics
{
    public partial class InventorySlotUI : MonoBehaviour
    {
        [SerializeField]
        [AutoEvent(nameof(InventorySlot.ItemChanged), nameof(OnItemChanged))]
        private InventorySlot slot;

        [SerializeField]
        private Image image;

        [SerializeField]
        private Image highlight;

        private void OnEnable()
        {
            __AutoOnEnable();

            OnItemChanged(slot.Item);
        }

        public void Highlight()
        {
            highlight.gameObject.SetActive(true);
        }

        public void Unhighlight()
        {
            highlight.gameObject.SetActive(false);
        }

        private void OnItemChanged(Item item)
        {
            if (item == null)
            {
                image.sprite = null;
                return;
            }

            image.sprite = item.Data.Sprite;
        }
    }
}
