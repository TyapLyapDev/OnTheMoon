using System;

namespace OnTheMoon.Runtime.Game
{
    public class PlayerBuilder : IPlayerBuilder
    {
        private readonly ICharacter _character;
        private readonly Player _player;
        private readonly IInputReader _inputReader;

        public PlayerBuilder(ICharacter character, IInputReader inputReader)
        {
            _character = character ?? throw new ArgumentNullException(nameof(character));

            _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
            _player = character.Transform.gameObject.AddComponent<Player>();
        }

        public IPlayerBuilder WithName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException($"{nameof(name)} В качестве имени возвращено пустое значение: '{name}'");

            _player.gameObject.name = name;

            return this;
        }

        public IPlayer Build()
        {
            _player.Initialize(_inputReader, _character);

            return _player;
        }
    }
}