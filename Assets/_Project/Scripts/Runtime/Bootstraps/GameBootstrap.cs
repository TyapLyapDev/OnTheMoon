using System;
using OnTheMoon.Runtime.Services;
using VContainer.Unity;

namespace OnTheMoon.Runtime.Game
{
    public class GameBootstrap : IStartable
    {
        private readonly PlayerStartPosition _playerStartPosition;
        private readonly ICursorLocker _cursorLocker;
        private readonly IViewpointPresenceSwitcher _viewpointPresenceSwitcher;
        private readonly ICharacterBuilder _characterBuilder;
        private readonly IGravitySource _gravitySource;
        private readonly IInputReader _inputReader;

        public GameBootstrap(
            PlayerStartPosition playerStartPosition,
            ICursorLocker cursorLocker,
            IViewpointPresenceSwitcher viewpointPresenceSwitcher,
            ICharacterBuilder characterBuilder,
            IGravitySource gravitySource,
            IInputReader inputReader)
        {
            _playerStartPosition = playerStartPosition != null ? playerStartPosition : throw new ArgumentNullException(nameof(playerStartPosition));
            _cursorLocker = cursorLocker ?? throw new ArgumentNullException(nameof(cursorLocker));
            _viewpointPresenceSwitcher = viewpointPresenceSwitcher ?? throw new ArgumentNullException(nameof(viewpointPresenceSwitcher));
            _characterBuilder = characterBuilder ?? throw new ArgumentNullException(nameof(characterBuilder));
            _gravitySource = gravitySource ?? throw new ArgumentNullException(nameof(gravitySource));
            _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
        }

        public void Start()
        {
            _cursorLocker.Lock();

            ICharacter character = _characterBuilder
                .WithPositionAndRotation(_playerStartPosition.transform)
                .WithGravitySource(_gravitySource)
                .Build();

            IPlayerBuilder playerBuilder = new PlayerBuilder(character, _inputReader);

            IPlayer player = playerBuilder
                .WithName(Constants.PlayerName)
                .Build();

            _viewpointPresenceSwitcher.SetCharacter(character);
            _viewpointPresenceSwitcher.SetMode(ViewpointPresenceType.ThirdPerson);
            _viewpointPresenceSwitcher.Enable();
        }
    }
}