using System;
using UnityEngine;

public class GroundChecker : IGroundChecker
{
    private readonly Transform _transform;
    private readonly IGravityBody _gravityBody;
    private readonly float _checkDistance;
    private readonly float _sphereRadius;
    private readonly int _layerMask;

    public GroundChecker(Transform transform, IGravityBody gravityBody, float checkDistance, float sphereRadius)
    {
        _transform = transform != null ? transform : throw new ArgumentNullException(nameof(transform));
        _gravityBody = gravityBody ?? throw new ArgumentNullException(nameof(gravityBody));

        if(checkDistance < 0)
            throw new ArgumentOutOfRangeException(nameof(checkDistance), checkDistance, "Значение должно быть положительным");

        _checkDistance = checkDistance;

        if (sphereRadius < 0)
            throw new ArgumentOutOfRangeException(nameof(sphereRadius), sphereRadius, "Значение должно быть больше нуля");

        _sphereRadius = sphereRadius;
        _layerMask = ~LayerMask.GetMask("Player");
    }

    public bool IsGround()
    {
        Vector3 gravityDirection = _gravityBody.GetGravityDirection();
        
        return Physics.SphereCast(_transform.position, _sphereRadius, gravityDirection, out RaycastHit _, _checkDistance, _layerMask);
    }
}