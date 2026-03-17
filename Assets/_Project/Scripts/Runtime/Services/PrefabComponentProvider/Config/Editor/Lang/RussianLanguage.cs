#if UNITY_EDITOR
using System.Collections.Generic;

namespace OnTheMoon.Runtime.Services.PrefabComponentProvider.Config.Editor.Lang
{
    public class RussianLanguage : ILanguageStrategy
    {
        private static readonly Dictionary<string, string> Strings = new()
        {
            [Localization.Keys.TypePathPairs] = "Пары тип-путь",
            [Localization.Keys.Element] = "Элемент {0}",
            [Localization.Keys.Prefab] = "Префаб",
            [Localization.Keys.TypeName] = "Имя типа",
            [Localization.Keys.Path] = "Путь",
            [Localization.Keys.Remove] = "Удалить",
            [Localization.Keys.AddNewPair] = "Добавить пару",
            [Localization.Keys.CheckPrefabsText] = "Проверить префабы",
            [Localization.Keys.NoPrefabSelected] = "Префаб не выбран",
            [Localization.Keys.NoComponentsFound] = "Компоненты не найдены",
            [Localization.Keys.ErrorPrefabNotInResources] = "Префаб должен находиться в папке Resources. Текущий путь: {0}",
            [Localization.Keys.ErrorNoMonoBehaviours] = "На префабе '{0}' не найдено MonoBehaviour компонентов.",
            [Localization.Keys.ValidationNoPairs] = "Нет пар для проверки",
            [Localization.Keys.ValidationErrors] = "Ошибки: отсутствуют типы или префабы",
            [Localization.Keys.ValidationDuplicates] = "Найдены дубликаты типов: {0}",
            [Localization.Keys.ValidationPassed] = "Все проверки пройдены",
            [Localization.Keys.LogWarningTypeNotFound] = "Тип '{0}' не найден",
            [Localization.Keys.LogWarningPrefabNotFound] = "Префаб по пути '{0}' не найден",
            [Localization.Keys.LanguageLabel] = "Язык",
        };

        public string GetString(string key) =>
            Strings.TryGetValue(key, out var value) ? value : $"[{key}]";
    }
}
#endif