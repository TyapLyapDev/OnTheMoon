using System;

namespace OnTheMoon.Runtime.Game
{
    public class ViewpointPresenceSwitcher : IViewpointPresenceSwitcher
    {
        private readonly ICameraRoot _cameraRoot;
        private readonly IInputReader _inputReader;
        private readonly IViewpointPresenceStrategyFactory _strategyFactory;

        private ICharacter _character;
        private ViewpointPresenceType _currentMode;

        public ViewpointPresenceSwitcher(
            ICameraRoot cameraRoot,
            IInputReader inputReader,
            IViewpointPresenceStrategyFactory strategyFactory)
        {
            _cameraRoot = cameraRoot ?? throw new ArgumentNullException(nameof(cameraRoot));
            _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
            _strategyFactory = strategyFactory ?? throw new ArgumentNullException(nameof(strategyFactory));
        }

        public void Enable()
        {
            _inputReader.ViewpointPresenceSwitchPressed += SwitchMode;
            _cameraRoot.Enable();
        }

        public void Disable()
        {
            _inputReader.ViewpointPresenceSwitchPressed -= SwitchMode;
            _cameraRoot.Disable();
        }

        public void SetCharacter(ICharacter character) =>
            _character = character ?? throw new ArgumentNullException(nameof(character));

        public void SetMode(ViewpointPresenceType mode)
        {
            if (_character == null)
                throw new InvalidOperationException("Character not set. Call SetCharacter first.");

            _currentMode = mode;

            _strategyFactory.CreateStrategies(
                mode,
                _character,
                _cameraRoot);
        }

        private void SwitchMode()
        {
            var nextMode = _currentMode == ViewpointPresenceType.FirstPerson
                ? ViewpointPresenceType.ThirdPerson
                : ViewpointPresenceType.FirstPerson;

            SetMode(nextMode);
        }
    }
}