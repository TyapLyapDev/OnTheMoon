#if UNITY_EDITOR
using System.Reflection;
using UnityEngine;

namespace OnTheMoon.Runtime.Services.PrefabComponentProvider.Config.Editor
{
    public static class ConfigPairProvider
    {
        public static TypePathPair[] GetPairs(PrefabComponentProviderConfig config)
        {
            if (config == null)
                return null;

            FieldInfo pairsField = typeof(PrefabComponentProviderConfig).GetField(
                PrefabComponentProviderConfig.PairsPropertyArray,
                BindingFlags.NonPublic | BindingFlags.Instance);

            if (pairsField == null)
            {
                Debug.LogError($"Cannot find _pairs field in {nameof(PrefabComponentProviderConfig)}");

                return null;
            }

            return pairsField.GetValue(config) as TypePathPair[];
        }
    }
}
#endif