using PickMen.Interaction;
using Shears;
using System;
using UnityEngine;

namespace PickMen.Players
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private Ref<Item> item;

        public Ref<Item> Item => item;
    }
}
