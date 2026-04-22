using UnityEngine;

namespace PickMen.Players.Graphics
{
    public partial class PlayerModel : MonoBehaviour
    {
        [SerializeField]
        private Transform transformReference;

        private void LateUpdate()
        {
            var rotation = transformReference.eulerAngles;
            rotation.x = 0;
            rotation.z = 0;

            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
