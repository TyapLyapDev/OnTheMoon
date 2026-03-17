#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace OnTheMoon.Runtime.Services.PrefabComponentProvider.Config.Editor
{
    public class PrefabComponentProviderConfigEditorModel
    {
        private readonly SerializedProperty _pairsProperty;
        private readonly Dictionary<string, string[]> _cachedComponentNames = new();
        private ValidationResult _lastValidationResult;

        public PrefabComponentProviderConfigEditorModel(SerializedObject serializedObject)
        {
            _pairsProperty = serializedObject.FindProperty(PrefabComponentProviderConfig.PairsPropertyArray);

            EditorApplication.projectChanged += OnProjectChanged;
        }

        public SerializedProperty PairsProperty => _pairsProperty;

        public ValidationResult LastValidationResult => _lastValidationResult;

        ~PrefabComponentProviderConfigEditorModel() =>
            EditorApplication.projectChanged -= OnProjectChanged;

        public IReadOnlyDictionary<string, string[]> CachedComponentNames => _cachedComponentNames;

        public GameObject LoadPrefab(string path) =>
            PrefabResolver.LoadPrefab(path);

        public void UpdateComponentCache(string path, GameObject prefab)
        {
            if (prefab == null)
            {
                _cachedComponentNames[path] = null;

                return;
            }

            MonoBehaviour[] components = prefab.GetComponents<MonoBehaviour>();

            if (components.Length == 0)
            {
                _cachedComponentNames[path] = null;

                return;
            }

            string[] names = new string[components.Length];

            for (int i = 0; i < components.Length; i++)
                names[i] = components[i].GetType().FullName;

            _cachedComponentNames[path] = names;
        }

        public string[] GetCachedComponentNames(string path)
        {
            _cachedComponentNames.TryGetValue(path, out string[] names);

            return names;
        }

        public string GetResourcesRelativePath(string fullAssetPath)
        {
            int index = fullAssetPath.IndexOf(PathConstants.ResourcesFolder);

            if (index == -1)
                return null;

            string relative = fullAssetPath[(index + PathConstants.ResourcesFolder.Length)..];

            return Path.ChangeExtension(relative, null);
        }

        public void ValidateConfig(PrefabComponentProviderConfig config) =>
            _lastValidationResult = ConfigValidator.Validate(config);

        public TypePathPair[] GetPairsFromTarget(PrefabComponentProviderConfig target) =>
            ConfigPairProvider.GetPairs(target);

        public void ClearValidationResult() =>
            _lastValidationResult = null;

        private void OnProjectChanged()
        {
            _cachedComponentNames.Clear();
            ClearValidationResult();
        }
    }
}
#endif