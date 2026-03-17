using System;

namespace OnTheMoon.Runtime.Game
{
    public class CharacterData
    {
        private readonly float _movementSpeed;
        private readonly float _rotationSpeed;
        private readonly float _jumpForce;
        private readonly float _alignSpeed;

        public CharacterData(float movementSpeed, float rotationSpeed, float jumpForce, float alignSpeed)
        {
            if (movementSpeed < 0)
                throw new ArgumentOutOfRangeException(nameof(movementSpeed), movementSpeed, "Значение должно быть положительным");

            if (rotationSpeed < 0)
                throw new ArgumentOutOfRangeException(nameof(rotationSpeed), rotationSpeed, "Значение должно быть положительным");

            if (jumpForce < 0)
                throw new ArgumentOutOfRangeException(nameof(jumpForce), jumpForce, "Значение должно быть положительным");

            if (alignSpeed < 0)
                throw new ArgumentOutOfRangeException(nameof(alignSpeed), alignSpeed, "Значение должно быть положительным");

            _movementSpeed = movementSpeed;
            _rotationSpeed = rotationSpeed;
            _jumpForce = jumpForce;
            _alignSpeed = alignSpeed;
        }

        public float MovementSpeed => _movementSpeed;

        public float RotationSpeed => _rotationSpeed;

        public float JumpForce => _jumpForce;

        public float AlignSpeed => _alignSpeed;
    }
}