using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public interface IBodyRotationStrategy
    {
        void Rotate(Vector2 input, float deltaTime);
    }
}