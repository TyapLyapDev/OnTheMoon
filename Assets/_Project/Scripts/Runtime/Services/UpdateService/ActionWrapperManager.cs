using System;
using System.Collections.Generic;
using UnityEngine;

namespace OnTheMoon.Runtime.Services
{
    internal class ActionWrapperManager
    {
        private readonly UpdateHandlerCollection _handlerCollection;
        private readonly Dictionary<Action, Action<float>> _wrappers = new();

        public ActionWrapperManager(UpdateHandlerCollection handlerCollection)
        {
            _handlerCollection = handlerCollection;
        }

        public void Subscribe(Action handler, UpdateType updateType)
        {
            if (handler == null)
            {
                Debug.LogError($"{UpdateServiceConst.Mark} Попытка подписать null-делегат без параметров");

                return;
            }

            if (_wrappers.ContainsKey(handler))
            {
#if UNITY_EDITOR
                Debug.LogWarning($"{UpdateServiceConst.Mark} Подписчик \"{handler.Target}.{handler.Method.Name}\" уже подписан (без deltaTime).");
#endif
                return;
            }

            void Wrapper(float dt)
            {
                if (handler.Target is UnityEngine.Object obj && obj == null)
                {
                    Unsubscribe(handler, updateType);

                    return;
                }

                handler();
            }

            _wrappers[handler] = Wrapper;

            if (_handlerCollection.TryGetList(updateType, out var list))
                _handlerCollection.Add(list, Wrapper, updateType);
        }

        public void Unsubscribe(Action handler, UpdateType updateType)
        {
            if (handler == null)
            {
                Debug.LogError($"{UpdateServiceConst.Mark} Попытка отписать null-делегат без параметров");

                return;
            }

            if (_wrappers.TryGetValue(handler, out Action<float> wrapper))
            {
                if (_handlerCollection.TryGetList(updateType, out var list))
                    _handlerCollection.Remove(list, wrapper, updateType);

                _wrappers.Remove(handler);
            }
        }
    }
}