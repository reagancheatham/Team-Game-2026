using UnityEngine;

namespace PickMen.Interaction
{
    public class Teleporter : MonoBehaviour
    {
        public GameObject player;
        public GameObject TPPoint;
        public bool readyToTP = true;

        private void OnTriggerEnter(Collider other) {
            if (readyToTP) {
                var controller = player.GetComponent<CharacterController>();
                controller.enabled = false;
                player.transform.position = TPPoint.transform.position;
                controller.enabled = true;
                readyToTP = false;
            }
        }

        private void OnTriggerExit(Collider other) {
            readyToTP = true;
        }
    }
}
