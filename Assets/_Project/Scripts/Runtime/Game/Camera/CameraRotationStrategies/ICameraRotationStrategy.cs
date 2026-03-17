using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public interface ICameraRotationStrategy
    {
        void Rotate(Vector2 input, float deltaTime);
    }
}