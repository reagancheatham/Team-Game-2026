using PickMen.GameManagement;
using Shears.Detection;
using UnityEngine;


namespace PickMen.Interaction
{
    public class Onion : MonoBehaviour
    {
        [SerializeField] private AreaDetector3D detector;

        private void FixedUpdate() {
            if (!detector.Detect() || !detector.TryGetDetection(out Item item, true))
                return;

            Destroy(item.gameObject);
            ScoreManager.AddScore(12);

        }
    }
}
