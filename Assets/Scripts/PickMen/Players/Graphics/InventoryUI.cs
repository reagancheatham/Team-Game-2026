using Shears;
using Shears.UI;
using UnityEngine;

namespace PickMen.Players.Graphics
{
    public partial class InventoryUI : UIElement
    {
        [SerializeField]
        private PlayerInventory inventory;

        [SerializeField]
        private InventorySlotUI[] slots;

        private void Start()
        {
            slots[inventory.SelectedSlot.Value].Highlight();
        }

        protected override void BindRefs()
        {
            Bind(inventory.SelectedSlot, OnSlotChanged);
        }

        private void OnSlotChanged(RefChangeEvent<int> evt)
        {
            print("new value: " + evt.newValue);
            return;

            slots[evt.oldValue].Unhighlight();
            slots[evt.newValue].Highlight();
        }
    }
}
