using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public interface ICameraRoot
    {
        Transform Transform { get; }

        void SetFollowStrategy(ICameraFollowStrategy strategy);

        void SetRotationStrategy(ICameraRotationStrategy cameraRotationStrategy);

        void Enable();

        void Disable();
    }
}