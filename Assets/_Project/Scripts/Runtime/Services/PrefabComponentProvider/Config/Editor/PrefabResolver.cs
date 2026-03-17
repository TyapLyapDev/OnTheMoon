#if UNITY_EDITOR
using UnityEngine;

namespace OnTheMoon.Runtime.Services.PrefabComponentProvider.Config.Editor
{
    public static class PrefabResolver
    {
        public static GameObject LoadPrefab(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            return Resources.Load<GameObject>(path);
        }
    }
}
#endif