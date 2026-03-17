#if UNITY_EDITOR
namespace OnTheMoon.Runtime.Services.PrefabComponentProvider.Config.Editor.Lang
{
    public interface ILanguageStrategy
    {
        string GetString(string key);
    }
}
#endif