using System;
using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public class ThirdPersonCameraStrategy : ICameraFollowStrategy, ICameraRotationStrategy
    {
        private readonly Transform _transform;
        private readonly Transform _target;
        private readonly float _distance;
        private readonly float _heightOffset;
        private readonly float _sensitivity;
        private readonly float _smoothSpeed;
        private readonly float _minPitch;
        private readonly float _maxPitch;

        private float _currentYaw;
        private float _currentPitch;

        public ThirdPersonCameraStrategy(
            Transform transform,
            Transform target,
            float sensitivity,
            float smoothSpeed,
            float minPitch,
            float maxPitch,
            float distance,
            float heightOffset)
        {
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _sensitivity = sensitivity > 0 ? sensitivity : throw new ArgumentOutOfRangeException(nameof(sensitivity));
            _smoothSpeed = smoothSpeed > 0 ? smoothSpeed : throw new ArgumentOutOfRangeException(nameof(smoothSpeed));
            _minPitch = minPitch;
            _maxPitch = maxPitch;
            _distance = distance > 0 ? distance : throw new ArgumentOutOfRangeException(nameof(distance));
            _heightOffset = heightOffset;

            Vector3 offset = _transform.position - _target.position;
            Vector3 up = _target.up;
            Vector3 projected = Vector3.ProjectOnPlane(offset, up).normalized;
            _currentYaw = Vector3.SignedAngle(Vector3.ProjectOnPlane(_target.forward, up), projected, up);
            _currentPitch = Vector3.Angle(offset, up) - 90f; // приблизительно
        }

        public void Rotate(Vector2 input, float deltaTime)
        {
            _currentYaw += input.x * _sensitivity * deltaTime;
            _currentPitch -= input.y * _sensitivity * deltaTime;
            _currentPitch = Mathf.Clamp(_currentPitch, _minPitch, _maxPitch);
        }

        public void Follow(float deltaTime)
        {
            Vector3 up = _target.up;
            Quaternion yawRot = Quaternion.AngleAxis(_currentYaw, up);
            Vector3 right = Vector3.Cross(up, _target.forward).normalized;
            Quaternion pitchRot = Quaternion.AngleAxis(_currentPitch, right);
            Vector3 forward = yawRot * (pitchRot * Vector3.forward);
            Vector3 desiredOffset = forward * _distance + up * _heightOffset;
            Vector3 desiredPosition = _target.position + desiredOffset;

            _transform.position = Vector3.Lerp(_transform.position, desiredPosition, _smoothSpeed * deltaTime);
            Vector3 lookDirection = (_target.position - _transform.position).normalized;
            _transform.rotation = Quaternion.LookRotation(lookDirection, up);
        }
    }
}