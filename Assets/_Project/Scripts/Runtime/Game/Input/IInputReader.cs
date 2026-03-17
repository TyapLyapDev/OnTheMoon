using System;
using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public interface IInputReader
    {
        event Action ViewpointPresenceSwitchPressed;

        Vector2 Move { get; }

        Vector2 Look { get; }

        bool IsJump { get; }

        void Enable();

        void Disable();
    }
}