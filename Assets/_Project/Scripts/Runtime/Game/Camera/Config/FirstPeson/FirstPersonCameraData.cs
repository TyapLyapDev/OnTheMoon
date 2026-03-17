using System;

namespace OnTheMoon.Runtime.Game
{
    public class FirstPersonCameraData
    {
        private readonly float _sensitivity;
        private readonly float _minVerticalAngle;
        private readonly float _maxVerticalAngle;
        private readonly float _smoothSpeed;
        private readonly float _angleSpeedFactor;
        private readonly float _followSpeed;

        public FirstPersonCameraData(
            float minVerticalAngle,
            float maxVerticalAngle,
            float sensitivity,
            float smoothSpeed,
            float angleSpeedFactor,
            float followSpeed)
        {
            if (sensitivity < 0)
                throw new ArgumentOutOfRangeException(nameof(sensitivity));

            if (smoothSpeed < 0)
                throw new ArgumentOutOfRangeException(nameof(smoothSpeed));

            if (angleSpeedFactor < 0)
                throw new ArgumentOutOfRangeException(nameof(angleSpeedFactor));

            if (followSpeed < 0)
                throw new ArgumentOutOfRangeException(nameof(followSpeed));

            _sensitivity = sensitivity;
            _minVerticalAngle = minVerticalAngle;
            _maxVerticalAngle = maxVerticalAngle;
            _smoothSpeed = smoothSpeed;
            _angleSpeedFactor = angleSpeedFactor;
            _followSpeed = followSpeed;
        }

        public float Sensitivity => _sensitivity;

        public float MinVerticalAngle => _minVerticalAngle;

        public float MaxVerticalAngle => _maxVerticalAngle;

        public float SmoothSpeed => _smoothSpeed;

        public float AngleSpeedFactor => _angleSpeedFactor;

        public float FollowSpeed => _followSpeed;
    }
}