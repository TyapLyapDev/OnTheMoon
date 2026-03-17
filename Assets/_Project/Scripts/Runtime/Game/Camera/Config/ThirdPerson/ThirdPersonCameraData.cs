using System;

namespace OnTheMoon.Runtime.Game
{
    public class ThirdPersonCameraData
    {
        private readonly float _sensitivity;
        private readonly float _smoothSpeed;
        private readonly float _minVerticalAngle;
        private readonly float _maxVerticalAngle;
        private readonly float _angleSpeedFactor;
        private readonly float _distance;
        private readonly float _heightOffset;

        public ThirdPersonCameraData(
            float sensitivity,
            float smoothSpeed,
            float minVerticalAngle,
            float maxVerticalAngle,
            float angleSpeedFactor,
            float distance,
            float heightOffset)
        {
            if (sensitivity < 0)
                throw new ArgumentOutOfRangeException(nameof(sensitivity), sensitivity, "Значение должно быть положительным");

            if (angleSpeedFactor < 0)
                throw new ArgumentOutOfRangeException(nameof(angleSpeedFactor), angleSpeedFactor, "Значение должно быть положительным");

            if (distance < 0)
                throw new ArgumentOutOfRangeException(nameof(distance), distance, "Значение должно быть положительным");

            _sensitivity = sensitivity;
            _smoothSpeed = smoothSpeed;
            _minVerticalAngle = minVerticalAngle;
            _maxVerticalAngle = maxVerticalAngle;
            _angleSpeedFactor = angleSpeedFactor;
            _distance = distance;
            _heightOffset = heightOffset;
        }

        public float Sensitivity => _sensitivity;

        public float SmoothSpeed => _smoothSpeed;

        public float MinVerticalAngle => _minVerticalAngle;

        public float MaxVerticalAngle => _maxVerticalAngle;

        public float AngleSpeedFactor => _angleSpeedFactor;

        public float Distance => _distance;

        public float HeightOffset => _heightOffset;
    }
}