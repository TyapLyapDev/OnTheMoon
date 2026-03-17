using System;
using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    [RequireComponent(typeof(Rigidbody))]
    public class GravityBody : MonoBehaviour, IGravityBody
    {
        private IGravitySource _gravitySource;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            if (TryGetComponent(out _rigidbody) == false)
                throw new NullReferenceException(nameof(_rigidbody));

            _rigidbody.useGravity = false;
        }

        private void FixedUpdate()
        {
            if (_gravitySource == null)
                return;

            Vector3 gravity = _gravitySource.GetGravity(transform.position);
            _rigidbody.AddForce(gravity, ForceMode.Acceleration);
        }

        public Vector3 GetGravityDirection()
        {
            if (_gravitySource == null)
                return Vector3.zero;

            return _gravitySource.GetDirection(transform.position);
        }

        public void SetGravitySource(IGravitySource source) =>
            _gravitySource = source ?? throw new ArgumentNullException(nameof(source));
    }
}