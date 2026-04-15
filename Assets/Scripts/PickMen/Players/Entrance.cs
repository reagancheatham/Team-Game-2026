using UnityEngine;

namespace PickMen.Interaction
{
    public class Entrance : MonoBehaviour
    {
        public GameObject player;
        public GameObject TPPoint;

        private void OnTriggerEnter(Collider other) {
            player.transform.position = TPPoint.transform.position;
        }
    }
}
