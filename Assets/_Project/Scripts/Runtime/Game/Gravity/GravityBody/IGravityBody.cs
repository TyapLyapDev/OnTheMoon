using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public interface IGravityBody
    {
        Vector3 GetGravityDirection();
    }
}