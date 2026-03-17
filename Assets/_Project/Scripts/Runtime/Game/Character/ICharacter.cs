using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public interface ICharacter
    {
        Transform Transform { get; }

        Rigidbody Rigidbody { get; }

        IGravityBody GravityBody { get; }

        IGroundChecker GroundChecker { get; }

        void SetBodyRotationStrategy(IBodyRotationStrategy strategy);

        void SetMovementStrategy(IMovementStrategy strategy);

        void SetJumpStrategy(IJumpStrategy jumpStrategy);

        void ResetBodyRotationStrategy();

        void Move(Vector2 input, float deltaTime);

        void Rotate(Vector2 input, float deltaTime);

        bool TryJump();
    }
}