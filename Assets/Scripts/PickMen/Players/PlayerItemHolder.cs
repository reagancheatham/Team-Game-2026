using PickMen.Interaction;
using Shears;
using Shears.Input;
using UnityEngine;

namespace PickMen.Players
{
    public sealed partial class PlayerItemHolder : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput input;

        [SerializeField]
        private Transform hand;

        [SerializeField, ReadOnly]
        private Item heldItem;

        public Item HeldItem => heldItem;

        public void Hold(Item item)
        {
            if (heldItem != null)
                return;

            heldItem = item;

            if (heldItem == null)
                return;

            heldItem.transform.SetParent(hand);
            heldItem.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            heldItem.PickUp();
        }

        public Item ReleaseToInventory()
        {
            var previousItem = heldItem;

            heldItem = null;

            return previousItem;
        }

        public void ReleaseToGround()
        {
            if (heldItem == null)
                return;

            heldItem.transform.SetParent(null);
            heldItem.Release();
            heldItem = null;
        }
    }
}
