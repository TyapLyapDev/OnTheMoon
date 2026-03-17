using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    [CreateAssetMenu(
        fileName = nameof(FirstPersonCameraConfig),
        menuName = Constants.AssetMenuConfigPath + nameof(FirstPersonCameraConfig),
        order = 0)]
    public class FirstPersonCameraConfig : ScriptableObject
    {
        [SerializeField] private float _minVerticalAngle = -80f;
        [SerializeField] private float _maxVerticalAngle = 80f;
        [SerializeField][Min(0)] private float _sensitivity = 100f;
        [SerializeField][Min(0)] private float _smoothSpeed = 10f;
        [SerializeField][Min(0)] private float _angleSpeedFactor = 0.1f;
        [SerializeField][Min(0)] private float _followSpeed = 10f;

        public FirstPersonCameraData GetData() =>
            new(
                _minVerticalAngle,
                _maxVerticalAngle,
                _sensitivity,
                _smoothSpeed,
                _angleSpeedFactor,
                _followSpeed);
    }
}