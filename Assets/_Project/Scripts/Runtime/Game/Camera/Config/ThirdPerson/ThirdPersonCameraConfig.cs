using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    [CreateAssetMenu(
        fileName = nameof(ThirdPersonCameraConfig),
        menuName = Constants.AssetMenuConfigPath + nameof(ThirdPersonCameraConfig),
        order = 0)]
    public class ThirdPersonCameraConfig : ScriptableObject, IThirdPersonCameraConfig
    {
        [SerializeField] private float _minVerticalAngle = -30f;
        [SerializeField] private float _maxVerticalAngle = 70f;
        [SerializeField][Min(0)] private float _sensitivity = 100f;
        [SerializeField][Min(0)] private float _smoothSpeed = 10f;
        [SerializeField][Min(0)] private float _angleSpeedFactor = 0.1f;
        [SerializeField][Min(0)] private float _distance = 5f;
        [SerializeField] private float _heightOffset = 2f;

        public ThirdPersonCameraData GetData() =>
            new(
                _sensitivity,
                _smoothSpeed,
                _minVerticalAngle,
                _maxVerticalAngle,
                _angleSpeedFactor,
                _distance,
                _heightOffset);
    }
}