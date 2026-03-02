using UnityEngine;

public interface IInputReader
{
    Vector2 Move { get; }

    Vector2 Look { get; }

    bool Jump { get; }
}
