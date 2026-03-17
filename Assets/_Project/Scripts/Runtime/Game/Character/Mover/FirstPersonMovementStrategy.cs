using System;
using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public class FirstPersonMovementStrategy : IMovementStrategy
    {
        private readonly Transform _transform;
        private readonly Rigidbody _rigidbody;
        private readonly IGravityBody _gravityBody;
        private readonly float _speed;

        public FirstPersonMovementStrategy(Transform transform, Rigidbody rigidbody, IGravityBody gravityBody, float speed)
        {
            _transform = transform != null ? transform : throw new ArgumentNullException(nameof(transform));
            _rigidbody = rigidbody != null ? rigidbody : throw new ArgumentNullException(nameof(rigidbody));
            _gravityBody = gravityBody ?? throw new ArgumentNullException(nameof(gravityBody));

            if (speed < 0)
                throw new ArgumentOutOfRangeException(nameof(speed), speed, "Значение должно быть положительным");

            _speed = speed;
        }

        public void Move(Vector2 input, float deltaTime)
        {
            if (deltaTime < 0)
                throw new ArgumentOutOfRangeException(nameof(deltaTime), deltaTime, "Значение должно быть положительным");

            Vector3 gravityUp = -_gravityBody.GetGravityDirection();
            Vector3 direction = GetMovementDirection(input, gravityUp);
            Vector3 currentVelocity = _rigidbody.linearVelocity;
            Vector3 horizontalVelocity = Vector3.ProjectOnPlane(_speed * direction, gravityUp);
            Vector3 verticalVelocity = Vector3.Project(currentVelocity, gravityUp);
            _rigidbody.linearVelocity = horizontalVelocity + verticalVelocity;
        }

        private Vector3 GetMovementDirection(Vector2 moveInput, Vector3 gravityUp)
        {
            Vector3 forward = _transform.forward;
            Vector3 right = _transform.right;

            forward = Vector3.ProjectOnPlane(forward, gravityUp).normalized;
            right = Vector3.ProjectOnPlane(right, gravityUp).normalized;

            return (forward * moveInput.y + right * moveInput.x).normalized;
        }
    }
}