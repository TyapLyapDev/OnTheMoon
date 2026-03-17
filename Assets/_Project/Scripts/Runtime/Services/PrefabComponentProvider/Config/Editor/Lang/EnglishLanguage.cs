#if UNITY_EDITOR
using System.Collections.Generic;

namespace OnTheMoon.Runtime.Services.PrefabComponentProvider.Config.Editor.Lang
{
    public class EnglishLanguage : ILanguageStrategy
    {
        private static readonly Dictionary<string, string> Strings = new()
        {
            [Localization.Keys.TypePathPairs] = "Type-Path Pairs",
            [Localization.Keys.Element] = "Element {0}",
            [Localization.Keys.Prefab] = "Prefab",
            [Localization.Keys.TypeName] = "Type Name",
            [Localization.Keys.Path] = "Path",
            [Localization.Keys.Remove] = "Remove",
            [Localization.Keys.AddNewPair] = "Add New Pair",
            [Localization.Keys.CheckPrefabsText] = "Check Prefabs",
            [Localization.Keys.NoPrefabSelected] = "No prefab selected",
            [Localization.Keys.NoComponentsFound] = "No components found",
            [Localization.Keys.ErrorPrefabNotInResources] = "Prefab must be inside a Resources folder. Current path: {0}",
            [Localization.Keys.ErrorNoMonoBehaviours] = "No MonoBehaviour components found on prefab '{0}'.",
            [Localization.Keys.ValidationNoPairs] = "No pairs to check",
            [Localization.Keys.ValidationErrors] = "Errors: some types or prefabs missing",
            [Localization.Keys.ValidationDuplicates] = "Duplicate types found: {0}",
            [Localization.Keys.ValidationPassed] = "All checks passed",
            [Localization.Keys.LogWarningTypeNotFound] = "Type '{0}' not found",
            [Localization.Keys.LogWarningPrefabNotFound] = "Prefab at path '{0}' not found",
            [Localization.Keys.LanguageLabel] = "Language",
        };

        public string GetString(string key) =>
            Strings.TryGetValue(key, out var value) ? value : $"[{key}]";
    }
}
#endif