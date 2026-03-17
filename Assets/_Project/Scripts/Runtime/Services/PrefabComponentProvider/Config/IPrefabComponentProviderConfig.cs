using System;
using System.Collections.Generic;

namespace OnTheMoon.Runtime.Services.PrefabComponentProvider.Config
{
    public interface IPrefabComponentProviderConfig
    {
        Dictionary<Type, string> GetMap();
    }
}