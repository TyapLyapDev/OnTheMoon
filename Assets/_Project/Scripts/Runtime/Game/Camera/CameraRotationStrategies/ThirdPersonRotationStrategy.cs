using System;
using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public class ThirdPersonRotationStrategy : ICameraRotationStrategy
    {
        private readonly Transform _transform;
        private readonly Transform _target;
        private readonly float _sensitivity;
        private readonly float _minVerticalAngle;
        private readonly float _maxVerticalAngle;
        private readonly float _smoothSpeed;
        private readonly float _angleSpeedFactor;

        private float _xRotation;

        public ThirdPersonRotationStrategy(
            Transform transform,
            Transform target,
            float sensitivity,
            float smoothSpeed,
            float minVerticalAngle,
            float maxVerticalAngle,
            float angleSpeedFactor)
        {
            _transform = transform != null ? transform : throw new ArgumentNullException(nameof(transform));
            _target = target != null ? target : throw new ArgumentNullException(nameof(target));

            if (sensitivity < 0)
                throw new ArgumentOutOfRangeException(nameof(sensitivity), sensitivity, "Значение должно быть положительным");

            if (smoothSpeed < 0)
                throw new ArgumentOutOfRangeException(nameof(smoothSpeed), smoothSpeed, "Значение должно быть положительным");

            _sensitivity = sensitivity;
            _smoothSpeed = smoothSpeed;

            _minVerticalAngle = minVerticalAngle;
            _maxVerticalAngle = maxVerticalAngle;
            _angleSpeedFactor = angleSpeedFactor;
        }

        public void Rotate(Vector2 input, float deltaTime)
        {
            if (deltaTime < 0)
                throw new ArgumentOutOfRangeException(nameof(deltaTime), deltaTime, "Значение должно быть положительным");

            float lookY = input.y * _sensitivity * deltaTime;
            _xRotation -= lookY;
            _xRotation = Mathf.Clamp(_xRotation, _minVerticalAngle, _maxVerticalAngle);
            Quaternion targetRotation = _target.rotation * Quaternion.Euler(_xRotation, 0f, 0f);
            float angle = Quaternion.Angle(_transform.rotation, targetRotation);
            float speed = _smoothSpeed * (1f + angle * _angleSpeedFactor);
            float t = Mathf.Clamp01(speed * deltaTime);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, t);
        }
    }
}