using UnityEngine;

namespace OnTheMoon.Runtime.Services.PrefabComponentProvider
{
    public interface IPrefabComponentProvider
    {
        T Get<T>()
            where T : MonoBehaviour;

        bool TryGet<T>(out T component)
            where T : MonoBehaviour;
    }
}