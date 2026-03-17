using System;
using System.Collections.Generic;
using UnityEngine;

namespace OnTheMoon.Runtime.Services.PrefabComponentProvider
{
    using Config;

    public class PrefabComponentProvider : IDisposable, IPrefabComponentProvider
    {
        private readonly Dictionary<Type, string> _map;
        private readonly Dictionary<(Type type, string path), MonoBehaviour> _cachedComponents;

        private bool _disposed;

        public PrefabComponentProvider(IPrefabComponentProviderConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _map = config.GetMap();
            _cachedComponents = new();
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _cachedComponents.Clear();
            _map.Clear();
            _disposed = true;
        }

        public T Get<T>()
            where T : MonoBehaviour
        {
            CheckDisposed();

            if (_map.Count == 0)
                throw new Exception($"No types registered");

            if (_map.TryGetValue(typeof(T), out string path) == false)
                throw new Exception($"The type {typeof(T)} was not found in the dictionary of paths");

            return FindInternal<T>(path);
        }

        public bool TryGet<T>(out T component)
            where T : MonoBehaviour
        {
            component = null;

            if (_map.TryGetValue(typeof(T), out string path) == false)
                return false;

            return TryFindInternal(path, out component);
        }

        private T FindInternal<T>(string path)
            where T : MonoBehaviour
        {
            CheckDisposed();

            var key = (typeof(T), path);

            if (_cachedComponents.TryGetValue(key, out MonoBehaviour cached))
            {
                if (cached is T result)
                    return result;

                _cachedComponents.Remove(key);
            }

            GameObject prefabGO = Resources.Load<GameObject>(path);

            if (prefabGO == null)
                throw new Exception($"The prefab on the path '{path}' was not found");

            if (prefabGO.TryGetComponent<T>(out T component) == false)
                throw new Exception($"The prefab '{path}' does not contain the {typeof(T)} component");

            _cachedComponents[key] = component;

            return component;
        }

        private bool TryFindInternal<T>(string path, out T component)
            where T : MonoBehaviour
        {
            component = null;
            var key = (typeof(T), path);

            if (_cachedComponents.TryGetValue(key, out var cached))
            {
                if (cached is T result)
                {
                    component = result;

                    return true;
                }
                _cachedComponents.Remove(key);
            }

            GameObject prefabGO = Resources.Load<GameObject>(path);

            if (prefabGO == null)
                return false;

            if (prefabGO.TryGetComponent<T>(out component) == false)
                return false;

            _cachedComponents[key] = component;

            return true;
        }

        private void CheckDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(PrefabComponentProvider));
        }
    }
}