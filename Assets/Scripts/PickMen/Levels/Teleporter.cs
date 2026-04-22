using PickMen.Interaction;
using UnityEngine;

namespace PickMen.Levels
{
    public partial class Teleporter : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private Transform targetPosition;

        public Transform TargetPosition => targetPosition;
    }
}
