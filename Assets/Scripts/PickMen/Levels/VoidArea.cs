using PickMen.Interaction;
using Shears.Detection;
using UnityEngine;

namespace PickMen.Levels
{
    public class VoidArea : MonoBehaviour
    {
        [SerializeField]
        private AreaDetector3D areaDetector;

        private void FixedUpdate()
        {
            if (!areaDetector.Detect() || !areaDetector.TryGetDetection(out IKillable killable))
                return;

            killable.Kill();
        }
    }
}
