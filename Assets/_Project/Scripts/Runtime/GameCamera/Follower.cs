using System;
using UnityEngine;

namespace Game.Camera
{
    public class Follower : MonoBehaviour
    {
        private Transform _target;

        public void SetTarget(Transform target) =>
            _target = target != null ? target : throw new ArgumentNullException(nameof(target));

        private void LateUpdate()
        {
            if (_target != null)
                transform.position = _target.position;
        }
    }
}