using UnityEngine;

public interface ICharacter
{
    void Move(Vector2 input);

    void Look(Vector2 input, float sensitivity, float deltaTime);

    void Jump();
}