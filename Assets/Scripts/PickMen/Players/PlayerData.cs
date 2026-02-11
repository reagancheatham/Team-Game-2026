using Shears;
using UnityEngine;

namespace PickMen.Players
{
    [CreateAssetMenu(menuName = "PickMen/Player/Data")]
    public partial class PlayerData : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField, AutoProperty]
        private float walkSpeed = 5.0f;

        [SerializeField, AutoProperty]
        private float sprintSpeed = 8.0f;

        [SerializeField, AutoProperty]
        private float crouchSpeed = 3.0f;

        [SerializeField, AutoProperty]
        private float airSpeed = 4.0f;

        [SerializeField, AutoProperty]
        private float jumpPower = 4.0f;

        [SerializeField, Min(0.25f), AutoProperty]
        private float crouchHeight = 1.0f;
    }
}
