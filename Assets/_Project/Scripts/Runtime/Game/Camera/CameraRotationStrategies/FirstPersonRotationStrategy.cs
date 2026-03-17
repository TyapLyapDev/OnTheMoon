using System;
using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public class FirstPersonRotationStrategy : ICameraRotationStrategy
    {
        private readonly Transform _transform;
        private readonly float _sensitivity;
        private readonly float _minVerticalAngle;
        private readonly float _maxVerticalAngle;

        private float _xRotation;
        private float _yRotation;

        public FirstPersonRotationStrategy(
            Transform transform,
            float sensitivity,
            float minVerticalAngle,
            float maxVerticalAngle)
        {
            _transform = transform != null ? transform : throw new ArgumentNullException(nameof(transform));

            if (sensitivity < 0)
                throw new ArgumentOutOfRangeException(nameof(sensitivity), sensitivity, "Значение должно быть положительным");

            _sensitivity = sensitivity;
            _minVerticalAngle = minVerticalAngle;
            _maxVerticalAngle = maxVerticalAngle;
            _yRotation = transform.rotation.y;
        }

        public void Rotate(Vector2 input, float deltaTime)
        {
            if (deltaTime < 0)
                throw new ArgumentOutOfRangeException(nameof(deltaTime), deltaTime, "Значение должно быть положительным");

            float lookY = input.y * _sensitivity * deltaTime;
            float lookX = input.x * _sensitivity * deltaTime;

            _xRotation -= lookY;
            _xRotation = Mathf.Clamp(_xRotation, _minVerticalAngle, _maxVerticalAngle);
            _yRotation += lookX;

            _transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        }
    }
}