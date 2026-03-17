using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public interface IMovementStrategy
    {
        void Move(Vector2 input, float deltaTime);
    }
}