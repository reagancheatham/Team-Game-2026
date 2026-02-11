using PickMen.Interaction;
using Shears;
using UnityEngine;
using UnityEngine.XR;

namespace PickMen.Players
{
    public class PlayerItemHolder : MonoBehaviour
    {
        [SerializeField]
        private Transform hand;

        [SerializeField, ReadOnly]
        private Pickup heldItem;

        public Pickup HeldItem => heldItem;

        public void PickUp(Pickup item)
        {
            if (heldItem != null)
                return;

            heldItem = item;
            heldItem.transform.SetParent(hand);
            heldItem.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            heldItem.PickUp();
        }

        public void Release()
        {
            if (heldItem == null)
                return;

            heldItem.transform.SetParent(null);
            heldItem.Release();
            heldItem = null;
        }
    }
}
