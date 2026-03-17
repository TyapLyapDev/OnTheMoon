using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public interface IGravitySource
    {
        Vector3 GetDirection(Vector3 position);

        Vector3 GetGravity(Vector3 position);
    }
}