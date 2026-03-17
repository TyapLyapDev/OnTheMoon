using System;
using UnityEngine;

namespace OnTheMoon.Runtime.Services.PrefabComponentProvider.Config
{
    [Serializable]
    public struct TypePathPair
    {
#if UNITY_EDITOR
        public const string TypeNameProperty = nameof(_typeName);
        public const string PathProperty = nameof(_path);
#endif

        [Tooltip("Полное название типа компонента, например: 'MyGame.PlayerComponent'")]
        [SerializeField] private string _typeName;

        [Tooltip("Путь к сборке в папке Ресурсов (без расширения)")]
        [SerializeField] private string _path;

        public readonly string TypeName => _typeName;

        public readonly string Path => _path;
    }
}