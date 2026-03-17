using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public interface ICharacterBuilder
    {
        ICharacterBuilder WithPosition(Vector3 position);

        ICharacterBuilder WithRotation(Quaternion rotation);

        ICharacterBuilder WithPositionAndRotation(Vector3 position, Quaternion rotation);

        ICharacterBuilder WithPositionAndRotation(Transform target);

        ICharacterBuilder WithGravitySource(IGravitySource source);

        ICharacter Build();
    }
}