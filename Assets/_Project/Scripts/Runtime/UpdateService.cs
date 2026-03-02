using System;
using UnityEngine;

public class UpdateService : MonoBehaviour, IUpdateService
{
    public event Action<float> Updated;
    public event Action<float> FixedUpdated;
    public event Action<float> LateUpdated;

    private void Update() =>
        Updated?.Invoke(Time.deltaTime);

    private void FixedUpdate() =>
        FixedUpdated?.Invoke(Time.fixedDeltaTime);

    private void LateUpdate() =>
        LateUpdated?.Invoke(Time.deltaTime);
}