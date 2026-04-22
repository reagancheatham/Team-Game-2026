using PickMen.Interaction;
using Shears;
using Shears.Input;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PickMen.Players
{
    public sealed partial class PlayerInventory : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput input;

        [SerializeField]
        private PlayerItemHolder itemHolder;

        [SerializeField, ReadOnly]
        private Ref<int> selectedSlot;

        [SerializeField]
        private List<InventorySlot> slots = new();

        [AutoEvent(nameof(IManagedInput.PerformedWithInfo), nameof(OnScrollSlotInput))]
        private IManagedInput scrollInventoryInput;

        [AutoEvent(nameof(IManagedInput.Performed), nameof(OnDropInput))]
        private IManagedInput dropInput;

        public Ref<int> SelectedSlot => selectedSlot;

        private void Awake()
        {
            scrollInventoryInput = input.ScrollInventory;
            dropInput = input.DropInput;
        }

        private void Start()
        {
            UpdateHeldItem();
        }

        public void AddItem(Item item)
        {
            var currentSlot = slots[selectedSlot.Value];

            if (currentSlot.Item.Value == null)
            {
                currentSlot.Item.Value = item;
                currentSlot.Item.Value.gameObject.SetActive(false);

                UpdateHeldItem();
                return;
            }

            foreach (var slot in slots)
            {
                if (slot.Item.Value == null)
                {
                    slot.Item.Value = item;
                    slot.Item.Value.gameObject.SetActive(false);

                    UpdateHeldItem();
                    return;
                }
            }
        }

        private void OnScrollSlotInput(ManagedInputInfo info)
        {
            int scrollValue = Mathf.RoundToInt(info.Input.ReadValue<Vector2>().y);

            int newValue = selectedSlot.Value + scrollValue;
            newValue = (newValue % slots.Count + slots.Count) % slots.Count;

            selectedSlot.Value = newValue;
            UpdateHeldItem();
        }
        
        private void OnDropInput()
        {
            if (slots[selectedSlot.Value].Item.Value == null)
                return;

            slots[selectedSlot.Value].Item.Value = null;
            itemHolder.ReleaseToGround();
        }

        private void UpdateHeldItem()
        {
            var slot = slots[selectedSlot.Value];

            if (itemHolder.HeldItem == slot.Item.Value)
                return;

            if (itemHolder.HeldItem != null)
            {
                var oldItem = itemHolder.ReleaseToInventory();
                oldItem.gameObject.SetActive(false);
            }

            if (slot.Item.Value != null)
            {
                slot.Item.Value.gameObject.SetActive(true);
                itemHolder.Hold(slot.Item.Value);
            }
        }
    }
}
