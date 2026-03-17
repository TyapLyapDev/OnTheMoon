#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace OnTheMoon.Runtime.Services.PrefabComponentProvider.Config.Editor.Lang
{
    public static partial class Localization
    {
        private const string EditorPrefKey = nameof(PrefabComponentProvider) + "_Language";

        private static ILanguageStrategy s_СurrentStrategy;

        public static Language CurrentLanguage
        {
            get => (Language)EditorPrefs.GetInt(EditorPrefKey, (int)Language.English);
            set
            {
                EditorPrefs.SetInt(EditorPrefKey, (int)value);
                s_СurrentStrategy = CreateStrategy(value);
            }
        }

        private static ILanguageStrategy CreateStrategy(Language language)
        {
            return language switch
            {
                Language.English => new EnglishLanguage(),
                Language.Russian => new RussianLanguage(),
                Language.RussianExpletive => new RussianExpletiveLanguage(),
                _ => new EnglishLanguage()
            };
        }

        static Localization()
        {
            s_СurrentStrategy = CreateStrategy(CurrentLanguage);
        }

        public static string Get(string key) =>
            s_СurrentStrategy.GetString(key);

        public static GUIContent GetContent(string key, string tooltipKey = null)
        {
            string text = Get(key);
            string tooltip = string.IsNullOrEmpty(tooltipKey) ? string.Empty : Get(tooltipKey);

            return new GUIContent(text, tooltip);
        }
    }
}
#endif