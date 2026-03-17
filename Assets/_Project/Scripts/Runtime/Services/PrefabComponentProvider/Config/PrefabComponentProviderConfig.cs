using System;
using System.Collections.Generic;
using OnTheMoon.Runtime.Game;
using UnityEngine;

namespace OnTheMoon.Runtime.Services.PrefabComponentProvider.Config
{
    [CreateAssetMenu(
        fileName = nameof(PrefabComponentProviderConfig),
        menuName = Constants.AssetMenuConfigPath + nameof(PrefabComponentProviderConfig),
        order = 0)]
    public partial class PrefabComponentProviderConfig : ScriptableObject, IPrefabComponentProviderConfig
    {
#if UNITY_EDITOR
        public const string PairsPropertyArray = nameof(_pairs);
#endif

        [SerializeField] private TypePathPair[] _pairs;

        public Dictionary<Type, string> GetMap()
        {
            var dictionary = new Dictionary<Type, string>();

            foreach (var pair in _pairs)
            {
                if (string.IsNullOrWhiteSpace(pair.TypeName))
                    throw new Exception($"Type name is empty in {nameof(PrefabComponentProviderConfig)}");

                Type type = TypeUtility.FindType(pair.TypeName) ?? throw new Exception($"Type '{pair.TypeName}' not found. Ensure the full type name (including namespace) is correct.");

                if (typeof(MonoBehaviour).IsAssignableFrom(type) == false)
                    throw new Exception($"Type '{pair.TypeName}' is not a MonoBehaviour.");

                if (string.IsNullOrWhiteSpace(pair.Path))
                    throw new Exception($"Path is empty for type '{pair.TypeName}'.");

                dictionary[type] = pair.Path;
            }

            return dictionary;
        }
    }
}