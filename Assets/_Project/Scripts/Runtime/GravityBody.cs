using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour, IGravityBody
{
    [SerializeField] private GravitySource _gravitySource;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
    }

    private void FixedUpdate()
    {
        if (_gravitySource == null) 
            return;

        Vector3 gravity = _gravitySource.GetGravity(transform.position);
        _rigidbody.AddForce(gravity, ForceMode.Acceleration);
    }

    public Vector3 GetGravityDirection()
    {
        if (_gravitySource == null) 
            return Vector3.zero;

        return (_gravitySource.transform.position - transform.position).normalized;
    }

    public void SetGravitySource(GravitySource source) =>
        _gravitySource = source != null ? source : throw new ArgumentNullException(nameof(source));
}