using System;
using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public class ThirdPersonBodyRotationStrategy : IBodyRotationStrategy
    {
        public void Rotate(Vector2 input, float deltaTime)
        {
            if (deltaTime < 0)
                throw new ArgumentOutOfRangeException(nameof(deltaTime), deltaTime, "Значение должно быть положительным");

            throw new NotImplementedException();
        }
    }
}