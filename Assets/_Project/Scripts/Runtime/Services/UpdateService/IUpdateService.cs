using System;

namespace OnTheMoon.Runtime.Services
{
    public interface IUpdateService
    {
        IUpdateService Subscribe(Action<float> handler, UpdateType updateType);

        IUpdateService Subscribe(Action handler, UpdateType updateType);

        IUpdateService Unsubscribe(Action<float> handler, UpdateType updateType);

        IUpdateService Unsubscribe(Action handler, UpdateType updateType);

        IUpdateService DebugPrint();
    }
}