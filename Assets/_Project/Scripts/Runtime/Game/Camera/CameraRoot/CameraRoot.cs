using System;
using OnTheMoon.Runtime.Services;
using UnityEngine;
using VContainer;

namespace OnTheMoon.Runtime.Game
{
    public class CameraRoot : MonoBehaviour, ICameraRoot
    {
        private IInputReader _inputReader;
        private IUpdateService _updateService;
        private ICameraRotationStrategy _cameraRotationStrategy;
        private ICameraFollowStrategy _cameraFollowStrategy;

        public Transform Transform => transform;

        [Inject]
        public void Construct(IInputReader inputReader, IUpdateService updateService)
        {
            _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
        }

        public void Enable() =>
            _updateService?.Subscribe(FixedTick, UpdateType.FixedUpdate);

        public void Disable() =>
            _updateService?.Unsubscribe(FixedTick, UpdateType.FixedUpdate);

        public void SetFollowStrategy(ICameraFollowStrategy strategy) =>
            _cameraFollowStrategy = strategy ?? throw new ArgumentNullException(nameof(strategy));

        public void SetRotationStrategy(ICameraRotationStrategy strategy) =>
            _cameraRotationStrategy = strategy ?? throw new ArgumentNullException(nameof(strategy));


        private void FixedTick(float fixedDeltaTime)
        {
            _cameraRotationStrategy?.Rotate(_inputReader.Look, fixedDeltaTime);
            _cameraFollowStrategy?.Follow(fixedDeltaTime);
        }
    }
}