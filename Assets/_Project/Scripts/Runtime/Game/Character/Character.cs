using System;
using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(GravityBody))]
    public class Character : MonoBehaviour, ICharacter
    {
        [SerializeField] private float _groundCheckDistance = -0.35f;
        [SerializeField] private float _groundSphereRadius = 0.45f;

        private Transform _transform;
        private Rigidbody _rigidbody;
        private GravityBody _gravityBody;
        private IMovementStrategy _movementStrategy;
        private IBodyRotationStrategy _bodyRotationStrategy;
        private IJumpStrategy _jumpStrategy;
        private IGroundChecker _groundChecker;
        private bool _isInitialized;

        public Transform Transform => _transform != null ? _transform : transform;

        public Rigidbody Rigidbody => _rigidbody != null ? _rigidbody : GetComponent<Rigidbody>();

        public IGravityBody GravityBody => _gravityBody;

        public IGroundChecker GroundChecker => _groundChecker;

        public void Initialize()
        {
            if (_isInitialized)
                return;

            _isInitialized = true;

            _transform = transform;
            _gravityBody = GetComponent<GravityBody>();
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.freezeRotation = true;

            _groundChecker = new GroundChecker(transform,
                _gravityBody,
                _groundCheckDistance,
                _groundSphereRadius);
        }

        public void SetBodyRotationStrategy(IBodyRotationStrategy strategy) =>
            _bodyRotationStrategy = strategy ?? throw new ArgumentNullException(nameof(strategy));

        public void ResetBodyRotationStrategy() =>
            _bodyRotationStrategy = null;

        public void SetMovementStrategy(IMovementStrategy strategy) =>
            _movementStrategy = strategy ?? throw new ArgumentNullException(nameof(strategy));

        public void SetJumpStrategy(IJumpStrategy strategy) =>
            _jumpStrategy = strategy ?? throw new ArgumentNullException(nameof(strategy));

        public void Move(Vector2 input, float deltaTime) =>
            _movementStrategy?.Move(input, deltaTime);

        public void Rotate(Vector2 input, float deltaTime) =>
            _bodyRotationStrategy?.Rotate(input, deltaTime);

        public bool TryJump()
        {
            if (_jumpStrategy == null)
                return false;

            return _jumpStrategy.TryJump();
        }

        private void OnDrawGizmosSelected()
        {
            if (_gravityBody == null)
                return;

            Vector3 direction = _gravityBody.GetGravityDirection();

            if (direction == Vector3.zero)
                direction = Vector3.down;

            Gizmos.color = Color.yellow;
            Vector3 endPoint = transform.position + direction * _groundCheckDistance;

            Gizmos.DrawLine(transform.position, endPoint);
            Gizmos.DrawWireSphere(endPoint, _groundSphereRadius);
        }
    }
}