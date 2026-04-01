using PickMen.Interaction;
using Shears;
using Shears.Input;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PickMen.Players
{
    public partial class PlayerInventory : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput input;

        [SerializeField]
        private PlayerItemHolder itemHolder;

        [SerializeField]
        private List<InventorySlot> slots = new();

        [AutoEvent(nameof(IManagedInput.PerformedWithInfo), nameof(OnScrollSlotInput))]
        private IManagedInput scrollInventoryInput;

        private int selectedSlot = 0;

        public int SelectedSlot => selectedSlot;

        public event Action<int> SelectedSlotChanged;

        private void Awake()
        {
            scrollInventoryInput = input.ScrollInventory;
        }

        private void Start()
        {
            UpdateHeldItem();
        }

        public void AddItem(Item item)
        {
            foreach (var slot in slots)
            {
                if (slot.Item == null)
                {
                    slot.SetItem(item);
                    return;
                }
            }
        }

        private void OnScrollSlotInput(ManagedInputInfo info)
        {
            int scrollValue = Mathf.RoundToInt(info.Input.ReadValue<float>());

            selectedSlot += scrollValue;
            selectedSlot %= slots.Count;

            UpdateHeldItem();

            SelectedSlotChanged?.Invoke(selectedSlot);
        }

        private void UpdateHeldItem()
        {
            
        }
    }
}
