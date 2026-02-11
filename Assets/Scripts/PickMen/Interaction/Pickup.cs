using Shears;
using Shears.Detection;
using Shears.Tweens;
using System;
using UnityEngine;

namespace PickMen.Interaction
{
    public class Pickup : MonoBehaviour, IInteractable
    {
        private static readonly TweenData dropTweenData = new(0.2f, easingFunction: TweenEase.OutBounce);

        [SerializeField]
        private RayDetector3D detector;

        private Tween moveTween;
        private Tween rotationTween;
        private bool isHeld = false;

        public event Action PickedUp;
        public event Action Released;

        private void Update()
        {
            detector.transform.rotation = Quaternion.identity;
        }

        public void PickUp()
        {
            if (isHeld)
                return;

            moveTween.Dispose();
            rotationTween.Dispose();

            isHeld = true;
            PickedUp?.Invoke();
        }

        public void Release()
        {
            if (!isHeld)
                return;

            isHeld = false;
            Released?.Invoke();

            if (!detector.Detect())
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.With(x: 0, z: 0));
                return;
            }

            moveTween.Dispose();
            rotationTween.Dispose();

            Vector3 position = detector.GetHit(0).point;
            Quaternion rotation = Quaternion.Euler(transform.eulerAngles.With(x: 0, z: 0));

            moveTween = transform.DoMoveTween(position, dropTweenData);
            rotationTween = transform.DoRotateTween(rotation, true, dropTweenData);
        }
    }
}
