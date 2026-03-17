using System;
using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public class FirstPersonBodyRotationStrategy : IBodyRotationStrategy
    {
        private readonly Transform _transform;
        private readonly float _speed;

        public FirstPersonBodyRotationStrategy(Transform transform, float speed)
        {
            _transform = transform != null ? transform : throw new ArgumentNullException(nameof(transform));

            if (speed <= 0)
                throw new ArgumentOutOfRangeException(nameof(speed), speed, "Значение дожно быть больше нуля");

            _speed = speed;
        }

        public void Rotate(Vector2 input, float deltaTime)
        {
            if (deltaTime < 0)
                throw new ArgumentOutOfRangeException(nameof(deltaTime), deltaTime, "Значение должно быть положительным");

            float horizontal = input.x * _speed * deltaTime;
            _transform.Rotate(Vector3.up * horizontal, Space.Self);
        }
    }
}