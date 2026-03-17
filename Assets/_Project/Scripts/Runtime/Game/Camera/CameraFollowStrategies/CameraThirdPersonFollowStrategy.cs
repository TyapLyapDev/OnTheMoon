using System;

namespace OnTheMoon.Runtime.Game
{
    public class CameraThirdPersonFollowStrategy : ICameraFollowStrategy
    {
        public void Follow(float deltaTime)
        {
            if (deltaTime < 0)
                throw new ArgumentOutOfRangeException(nameof(deltaTime), deltaTime, "Значение должно быть положительным");

            throw new NotImplementedException();
        }
    }
}