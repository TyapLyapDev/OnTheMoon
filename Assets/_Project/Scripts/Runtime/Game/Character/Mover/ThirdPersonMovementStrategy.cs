using System;
using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public class ThirdPersonMovementStrategy : IMovementStrategy
    {
        private readonly Transform _transform;
        private readonly Rigidbody _rigidbody;
        private readonly IGravityBody _gravityBody;
        private readonly float _moveSpeed;
        private readonly float _rotationSpeed;
        private readonly Transform _cameraTransform;
        private readonly float _inputThreshold = 0.1f;

        public ThirdPersonMovementStrategy(
            Transform transform,
            Rigidbody rigidbody,
            IGravityBody gravityBody,
            float moveSpeed,
            float rotationSpeed,
            Transform cameraTransform)
        {
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
            _rigidbody = rigidbody ?? throw new ArgumentNullException(nameof(rigidbody));
            _gravityBody = gravityBody ?? throw new ArgumentNullException(nameof(gravityBody));
            _moveSpeed = moveSpeed >= 0 ? moveSpeed : throw new ArgumentOutOfRangeException(nameof(moveSpeed));
            _rotationSpeed = rotationSpeed >= 0 ? rotationSpeed : throw new ArgumentOutOfRangeException(nameof(rotationSpeed));
            _cameraTransform = cameraTransform ?? throw new ArgumentNullException(nameof(cameraTransform));
        }

        public void Move(Vector2 input, float deltaTime)
        {
            if (deltaTime < 0)
                throw new ArgumentOutOfRangeException(nameof(deltaTime), deltaTime, "Значение должно быть положительным");

            Vector3 gravityUp = -_gravityBody.GetGravityDirection();
            Vector3 moveDirection = GetMovementDirection(input, gravityUp);

            Vector3 currentVelocity = _rigidbody.linearVelocity;
            Vector3 horizontalVelocity = Vector3.ProjectOnPlane(_moveSpeed * moveDirection, gravityUp);
            Vector3 verticalVelocity = Vector3.Project(currentVelocity, gravityUp);
            _rigidbody.linearVelocity = horizontalVelocity + verticalVelocity;

            if (moveDirection.sqrMagnitude > _inputThreshold * _inputThreshold)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, gravityUp);
                _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, _rotationSpeed * deltaTime);
            }
        }

        private Vector3 GetMovementDirection(Vector2 input, Vector3 gravityUp)
        {
            Vector3 cameraForward = Vector3.ProjectOnPlane(_cameraTransform.forward, gravityUp).normalized;
            Vector3 cameraRight = Vector3.ProjectOnPlane(_cameraTransform.right, gravityUp).normalized;

            return (cameraForward * input.y + cameraRight * input.x).normalized;
        }
    }
}