using System;
using UnityEngine;

namespace Game.Camera
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float _sensitivity = 100f;
        [SerializeField] private float _minVerticalAngle = -80f;
        [SerializeField] private float _maxVerticalAngle = 80f;

        private Transform _target;
        private float _xRotation = 0f;

        public float Sensitivity => _sensitivity;

        public void SetTarget(Transform target) =>
            _target = target != null ? target : throw new ArgumentNullException(nameof(target));

        public void ApplyLook(Vector2 input, float deltaTime)
        {
            if (_target == null)
                return;

            float lookY = input.y * _sensitivity * deltaTime;
            _xRotation -= lookY;
            _xRotation = Mathf.Clamp(_xRotation, _minVerticalAngle, _maxVerticalAngle);
            Quaternion targetRotation = _target.rotation * Quaternion.Euler(_xRotation, 0f, 0f);
            transform.rotation = targetRotation;
        }
    }
}