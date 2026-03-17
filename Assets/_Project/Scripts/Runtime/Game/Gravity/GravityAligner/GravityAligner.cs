using System;
using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public class GravityAligner : MonoBehaviour, IGravityAligner
    {
        private GravityBody _gravityBody;
        private float _speed;
        private bool _isOn;

        private void FixedUpdate()
        {
            if (_isOn == false)
                return;

            Vector3 gravityUp = -_gravityBody.GetGravityDirection();

            if (gravityUp == Vector3.zero)
                return;

            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, gravityUp) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speed * Time.fixedDeltaTime);
        }

        public void Initialize(float speed)
        {
            if (speed < 0f)
                throw new ArgumentOutOfRangeException(nameof(speed), speed, "Значение должно быть положительным");

            if (TryGetComponent(out _gravityBody) == false)
                throw new NullReferenceException($"На объекте не найден компонент {nameof(GravityBody)}, требуемый для инициализации {nameof(GravityAligner)}");

            _speed = speed;
            Enable();
        }

        public void Enable() =>
            _isOn = true;

        public void Disable() =>
            _isOn = false;
    }
}