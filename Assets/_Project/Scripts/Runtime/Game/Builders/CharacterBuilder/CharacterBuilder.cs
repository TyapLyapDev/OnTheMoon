using System;
using OnTheMoon.Runtime.Services.PrefabComponentProvider;
using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public class CharacterBuilder : ICharacterBuilder
    {
        private readonly Character _character;
        private readonly ICharacterConfig _characterConfig;

        public CharacterBuilder(IPrefabComponentProvider prefabComponentProvider, ICharacterConfig characterConfig)
        {
            if (prefabComponentProvider == null)
                throw new ArgumentNullException(nameof(prefabComponentProvider));

            _characterConfig = characterConfig ?? throw new ArgumentNullException(nameof(characterConfig));

            Character prefab = prefabComponentProvider.Get<Character>();
            _character = UnityEngine.Object.Instantiate(prefab);
            _characterConfig = characterConfig;
        }

        public ICharacterBuilder WithPosition(Vector3 position)
        {
            _character.transform.position = position;

            return this;
        }

        public ICharacterBuilder WithRotation(Quaternion rotation)
        {
            _character.transform.rotation = rotation;

            return this;
        }

        public ICharacterBuilder WithPositionAndRotation(Transform target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            _character.transform.SetPositionAndRotation(target.position, target.rotation);

            return this;
        }

        public ICharacterBuilder WithPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            _character.transform.SetPositionAndRotation(position, rotation);

            return this;
        }

        public ICharacterBuilder WithGravitySource(IGravitySource source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (_character.TryGetComponent(out GravityBody gravityBody) == false)
                throw new NullReferenceException($"На объекте не найден компонент {nameof(GravityBody)}, требуемый для инициализации {nameof(GravityAligner)}");

            gravityBody.SetGravitySource(source);

            return this;
        }

        public ICharacter Build()
        {
            _character.Initialize();

            if (_character.TryGetComponent(out GravityAligner gravityAligner))
            {
                float alignSpeed = _characterConfig.GetData().AlignSpeed;
                gravityAligner.Initialize(alignSpeed);
                gravityAligner.Enable();
            }

            return _character;
        }
    }
}