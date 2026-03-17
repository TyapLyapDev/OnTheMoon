using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    [CreateAssetMenu(
        fileName = nameof(CharacterConfig),
        menuName = Constants.AssetMenuConfigPath + nameof(CharacterConfig),
        order = 0)]
    public class CharacterConfig : ScriptableObject, ICharacterConfig
    {
        [SerializeField][Min(0)] private float _movementSpeed;
        [SerializeField][Min(0)] private float _rotationSpeed;
        [SerializeField][Min(0)] private float _jumpForce;
        [SerializeField][Min(0)] private float _alignSpeed;

        public CharacterData GetData() =>
            new(
                _movementSpeed,
                _rotationSpeed,
                _jumpForce,
                _alignSpeed);
    }
}