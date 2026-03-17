using System;
using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public class CameraFirstPersonFollowStrategy : ICameraFollowStrategy
    {
        private readonly Transform _transform;
        private readonly Transform _target;
        private readonly float _speed;

        public CameraFirstPersonFollowStrategy(Transform transform, Transform target, float speed)
        {
            _transform = transform != null ? transform : throw new ArgumentNullException(nameof(transform));
            _target = target != null ? target : throw new ArgumentNullException(nameof(target));

            if (speed < 0)
                throw new ArgumentOutOfRangeException(nameof(speed), speed, "Значение должно быть положительным");

            _speed = speed;
        }

        public void Follow(float deltaTime)
        {
            if (deltaTime < 0)
                throw new ArgumentOutOfRangeException(nameof(deltaTime), deltaTime, "Значение должно быть положительным");

            _transform.position = Vector3.Lerp(
                _transform.position,
                _target.position,
                _speed * deltaTime);
        }
    }
}