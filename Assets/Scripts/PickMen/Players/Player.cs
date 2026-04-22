using PickMen.Interaction;
using Shears.Input;
using UnityEngine;

namespace PickMen.Players
{
    public class Player : MonoBehaviour, IKillable
    {
        [Header("Data")]
        [SerializeField]
        private PlayerData data;

        [Header("Components")]
        [SerializeField]
        private PlayerInput input;

        [SerializeField]
        private PlayerMovement movement;

        [SerializeField]
        private Transform spawn;

        private void Awake()
        {
            CursorManager.SetCursorVisibility(false);
            CursorManager.SetCursorLockMode(CursorLockMode.Locked);

            movement.Initialize(data, input);
        }

        private void Update()
        {
            movement.UpdateMovement();
        }

        public void Kill()
        {
            movement.Controller.enabled = false;
            transform.position = spawn.position;
            movement.Controller.enabled = true;
        }
    }
}
