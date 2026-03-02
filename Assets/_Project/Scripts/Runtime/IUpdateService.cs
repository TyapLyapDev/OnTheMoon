using System;

public interface IUpdateService
{
    event Action<float> Updated;

    event Action<float> FixedUpdated;

    event Action<float> LateUpdated;
}