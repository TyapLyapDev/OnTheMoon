using System;
using UnityEngine;

public class GravityAligner : IGravityAligner
{
    private readonly Transform _transform;
    private readonly GravityBody _gravityBody;
    private readonly IUpdateService _updateService;
    private readonly float _speed;

    public GravityAligner(Transform transform, GravityBody gravityBody, IUpdateService updateService, float speed)
    {
        _transform = transform != null ? transform : throw new ArgumentNullException(nameof(transform));
        _gravityBody = gravityBody != null ? gravityBody : throw new ArgumentNullException(nameof(gravityBody));
        _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));

        if (speed <= 0f)
            throw new ArgumentOutOfRangeException(nameof(speed));

        _speed = speed;
    }

    public void Enable() =>
        _updateService.LateUpdated += OnLateUpdate;

    public void Disable() =>
        _updateService.LateUpdated -= OnLateUpdate;

    private void OnLateUpdate(float deltaTime)
    {
        Vector3 gravityUp = -_gravityBody.GetGravityDirection();
        Quaternion targetRotation = Quaternion.FromToRotation(_transform.up, gravityUp) * _transform.rotation;
        _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, _speed * deltaTime);
    }
}