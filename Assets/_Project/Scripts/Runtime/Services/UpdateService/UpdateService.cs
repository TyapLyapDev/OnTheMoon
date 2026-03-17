using System;
using System.Collections.Generic;
using UnityEngine;

namespace OnTheMoon.Runtime.Services
{
    public class UpdateService : MonoBehaviour, IUpdateService
    {
#if UNITY_EDITOR
        [Header("Debug")]
        [SerializeField] private bool _logSubscribers;
#endif

        private UpdateHandlerCollection _handlerCollection;
        private ActionWrapperManager _wrapperManager;

        private void Awake()
        {
            _handlerCollection = new UpdateHandlerCollection(_logSubscribers);
            _wrapperManager = new ActionWrapperManager(_handlerCollection);
        }

        private void Update() =>
            _handlerCollection.Invoke(UpdateType.Update, Time.deltaTime);

        private void FixedUpdate() =>
            _handlerCollection.Invoke(UpdateType.FixedUpdate, Time.fixedDeltaTime);

        private void LateUpdate() =>
            _handlerCollection.Invoke(UpdateType.LateUpdate, Time.deltaTime);

        public IUpdateService Subscribe(Action<float> handler, UpdateType updateType)
        {
            if (_handlerCollection.TryGetList(updateType, out var list))
                _handlerCollection.Add(list, handler, updateType);

            return this;
        }

        public IUpdateService Subscribe(Action handler, UpdateType updateType)
        {
            _wrapperManager.Subscribe(handler, updateType);

            return this;
        }

        public IUpdateService Unsubscribe(Action<float> handler, UpdateType updateType)
        {
            if (_handlerCollection.TryGetList(updateType, out List<Action<float>> list))
                _handlerCollection.Remove(list, handler, updateType);

            return this;
        }

        public IUpdateService Unsubscribe(Action handler, UpdateType updateType)
        {
            _wrapperManager.Unsubscribe(handler, updateType);

            return this;
        }

        public IUpdateService DebugPrint()
        {
            Debug.Log(_handlerCollection.GetDebugString());

            return this;
        }
    }
}