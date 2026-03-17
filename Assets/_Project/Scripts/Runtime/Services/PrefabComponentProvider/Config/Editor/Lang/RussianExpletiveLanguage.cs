#if UNITY_EDITOR
using System.Collections.Generic;

namespace OnTheMoon.Runtime.Services.PrefabComponentProvider.Config.Editor.Lang
{
    public class RussianExpletiveLanguage : ILanguageStrategy
    {
        private static readonly Dictionary<string, string> Strings = new()
        {
            [Localization.Keys.TypePathPairs] = "Херня с типом и путём",
            [Localization.Keys.Element] = "Херь {0}",
            [Localization.Keys.Prefab] = "Кусок",
            [Localization.Keys.TypeName] = "Погоняло",
            [Localization.Keys.Path] = "Путь",
            [Localization.Keys.Remove] = "Нахер",
            [Localization.Keys.AddNewPair] = "Навалить",
            [Localization.Keys.CheckPrefabsText] = "Ответить за базар",
            [Localization.Keys.NoPrefabSelected] = "Просран кусок",
            [Localization.Keys.NoComponentsFound] = "Просрано погоняло",
            [Localization.Keys.ErrorPrefabNotInResources] = "Пропал кусок отсюда: {0}",
            [Localization.Keys.ErrorNoMonoBehaviours] = "На куске '{0}' нет монобеха",
            [Localization.Keys.ValidationNoPairs] = "В шары долбишься? Тут пусто",
            [Localization.Keys.ValidationErrors] = "Косяки: нет погоняла или куска",
            [Localization.Keys.ValidationDuplicates] = "Здесь повторы, алё! {0}",
            [Localization.Keys.ValidationPassed] = "Всё пучком",
            [Localization.Keys.LogWarningTypeNotFound] = "Погоняло '{0}' не по шарам",
            [Localization.Keys.LogWarningPrefabNotFound] = "Кусок отсюда '{0}' отвалился",
            [Localization.Keys.LanguageLabel] = "Базар",
        };

        public string GetString(string key) =>
            Strings.TryGetValue(key, out var value) ? value : $"[{key}]";
    }
}
#endif