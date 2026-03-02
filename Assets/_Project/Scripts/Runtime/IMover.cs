using UnityEngine;

public interface IMover
{
    void Move(Vector2 input, float speed);

    void Jump(float force, Vector3 gravityUp);
}