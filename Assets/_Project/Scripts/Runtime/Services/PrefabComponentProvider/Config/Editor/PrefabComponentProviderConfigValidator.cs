#if UNITY_EDITOR
using System;
using UnityEngine;

namespace OnTheMoon.Runtime.Services.PrefabComponentProvider.Config.Editor
{
    using Lang;

    public static class PrefabComponentProviderConfigValidator
    {
        public static void CheckPresenceComponentsSpecifiedPaths(PrefabComponentProviderConfig config)
        {
            TypePathPair[] pairs = ConfigPairProvider.GetPairs(config);

            if (pairs == null)
                return;

            foreach (TypePathPair pair in pairs)
            {
                if (string.IsNullOrEmpty(pair.TypeName) || string.IsNullOrEmpty(pair.Path))
                    continue;

                Type type = TypeUtility.FindType(pair.TypeName);

                if (type == null)
                    Debug.LogWarning(string.Format(Localization.Get(Localization.Keys.LogWarningTypeNotFound), pair.TypeName), config);

                GameObject prefab = PrefabResolver.LoadPrefab(pair.Path);

                if (prefab == null)
                    Debug.LogWarning(string.Format(Localization.Get(Localization.Keys.LogWarningPrefabNotFound), pair.Path), config);
            }
        }
    }
}
#endif