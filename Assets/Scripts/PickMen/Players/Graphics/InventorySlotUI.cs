using PickMen.Interaction;
using Shears;
using Shears.UI;
using UnityEngine;
using UnityEngine.UI;

namespace PickMen.Players.Graphics
{
    public sealed class InventorySlotUI : UIElement
    {
        [SerializeField]
        private InventorySlot slot;

        [SerializeField]
        private Image image;

        [SerializeField]
        private Image highlight;

        protected override void BindRefs()
        {
            Bind(slot.Item, OnItemChanged);
        }

        public void Highlight()
        {
            highlight.gameObject.SetActive(true);
        }

        public void Unhighlight()
        {
            highlight.gameObject.SetActive(false);
        }

        private void OnItemChanged(RefChangeEvent<Item> evt)
        {
            UpdateItemVisual(evt.newValue);
        }

        private void UpdateItemVisual(Item item)
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
