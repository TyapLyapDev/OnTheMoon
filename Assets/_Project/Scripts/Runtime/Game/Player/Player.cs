using System;
using OnTheMoon.Runtime.Services;
using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public class Player : MonoBehaviour, IPlayer
    {
        private ICharacter _character;
        private IInputReader _inputReader;

        public Transform Transform => transform;

        public void Initialize(IInputReader inputReader, ICharacter character)
        {
            _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
            _character = character ?? throw new ArgumentNullException(nameof(character));

            _inputReader.Enable();
        }

        public void FixedUpdate()
        {
            if (_inputReader == null)
                return;

            float deltaTime = Time.fixedDeltaTime;

            _character?.Move(_inputReader.Move, deltaTime);
            _character?.Rotate(_inputReader.Look, deltaTime);

            if (_inputReader.IsJump)
                _character?.TryJump();
        }
    }
}