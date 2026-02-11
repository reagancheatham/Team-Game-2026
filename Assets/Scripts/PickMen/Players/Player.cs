using Shears;
using Shears.Detection;
using Shears.Input;
using System.Collections;
using UnityEngine;

namespace PickMen.Players
{
    public class Player : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField]
        private PlayerData data;

        [Header("Components")]
        [SerializeField]
        private PlayerInput input;

        [SerializeField]
        private PlayerMovement movement;

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
    }
}
