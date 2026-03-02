using System;
using UnityEngine;

public class Mover : IMover
{
    private readonly Transform _transform;
    private readonly Rigidbody _rigidbody;
    private readonly IGravityBody _gravityBody;

    public Mover(Transform transform,Rigidbody rigidbody, IGravityBody gravityBody)
    {
        _transform = transform != null ? transform : throw new ArgumentNullException(nameof(transform));
        _rigidbody = rigidbody != null ? rigidbody : throw new ArgumentNullException(nameof(rigidbody));
        _gravityBody = gravityBody ?? throw new ArgumentNullException(nameof(gravityBody));
    }

    public void Move(Vector2 input, float speed)
    {
        Vector3 gravityUp = -_gravityBody.GetGravityDirection();
        Vector3 direction = GetMovementDirection(input, gravityUp);
        Vector3 currentVelocity = _rigidbody.linearVelocity;
        Vector3 horizontalVelocity = Vector3.ProjectOnPlane(direction * speed, gravityUp);
        Vector3 verticalVelocity = Vector3.Project(currentVelocity, gravityUp);
        _rigidbody.linearVelocity = horizontalVelocity + verticalVelocity;
    }

    public void Jump(float force, Vector3 gravityUp)
    {
        _rigidbody.AddForce(gravityUp * force, ForceMode.VelocityChange);
    }

    private Vector3 GetMovementDirection(Vector2 moveInput, Vector3 gravityUp)
    {
        Vector3 forward = _transform.forward;
        Vector3 right = _transform.right;

        forward = Vector3.ProjectOnPlane(forward, gravityUp).normalized;
        right = Vector3.ProjectOnPlane(right, gravityUp).normalized;

        return (forward * moveInput.y + right * moveInput.x).normalized;
    }
}