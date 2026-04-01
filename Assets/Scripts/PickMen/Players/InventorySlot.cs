using PickMen.Interaction;
using Shears;
using System;
using UnityEngine;

namespace PickMen.Players
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private Item item;

        public Item Item => item;

        public event Action<Item> ItemChanged;

        public void SetItem(Item item)
        {
            this.item = item;

            ItemChanged?.Invoke(item);
        }
    }
}
