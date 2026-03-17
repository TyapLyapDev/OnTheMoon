using System;
using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public class JumpStrategy : IJumpStrategy
    {
        private readonly Rigidbody _rigidbody;
        private readonly IGroundChecker _groundChecker;
        private readonly float _jumpForce;

        public JumpStrategy(Rigidbody rigidbody, IGroundChecker groundChecker, float jumpForce)
        {
            _rigidbody = rigidbody != null ? rigidbody : throw new ArgumentNullException(nameof(rigidbody));
            _groundChecker = groundChecker ?? throw new ArgumentNullException(nameof(groundChecker));

            if (jumpForce < 0)
                throw new ArgumentOutOfRangeException(nameof(jumpForce), jumpForce, "Значение должно быть положительным");

            _jumpForce = jumpForce;
        }

        public bool TryJump()
        {
            if (_groundChecker.IsGround() == false)
                return false;

            _rigidbody.AddForce(
                _rigidbody.transform.up * _jumpForce,
                ForceMode.VelocityChange);

            return true;
        }
    }
}