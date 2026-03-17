#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace OnTheMoon.Runtime.Services.PrefabComponentProvider.Config.Editor
{
    using Lang;

    public static class ConfigValidator
    {
        public static ValidationResult Validate(PrefabComponentProviderConfig config)
        {
            TypePathPair[] pairs = ConfigPairProvider.GetPairs(config);

            if (pairs == null || pairs.Length == 0)
            {
                return new ValidationResult(
                    hasError: false,
                    hasDuplicates: false,
                    message: Localization.Get(Localization.Keys.ValidationNoPairs),
                    color: Color.gray);
            }

            bool hasError = false;

            foreach (TypePathPair pair in pairs)
            {
                if (string.IsNullOrEmpty(pair.TypeName) || string.IsNullOrEmpty(pair.Path))
                {
                    hasError = true;
                    continue;
                }

                Type type = TypeUtility.FindType(pair.TypeName);
                if (type == null)
                    hasError = true;

                GameObject prefab = PrefabResolver.LoadPrefab(pair.Path);
                if (prefab == null)
                    hasError = true;
            }

            List<DuplicateInfo> duplicates = DuplicateChecker.FindDuplicates(pairs);
            bool hasDuplicates = duplicates.Count > 0;

            if (hasError)
            {
                return new ValidationResult(
                    hasError: true,
                    hasDuplicates: hasDuplicates,
                    message: Localization.Get(Localization.Keys.ValidationErrors),
                    color: Color.red,
                    duplicates: duplicates);
            }
            else if (hasDuplicates)
            {
                string details = string.Join("; ", duplicates.Select(d =>
                {
                    string paths = string.Join(", ", d.Pairs.Select(p => p.Path));
                    return $"{d.TypeName} (Paths: {paths})";
                }));

                string message = string.Format(Localization.Get(Localization.Keys.ValidationDuplicates), details);
                return new ValidationResult(
                    hasError: false,
                    hasDuplicates: true,
                    message: message,
                    color: Color.yellow,
                    duplicates: duplicates);
            }
            else
            {
                return new ValidationResult(
                    hasError: false,
                    hasDuplicates: false,
                    message: Localization.Get(Localization.Keys.ValidationPassed),
                    color: Color.green);
            }
        }
    }
}
#endif