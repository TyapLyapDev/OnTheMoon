using System;
using OnTheMoon.Runtime.Services;
using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    public class InputReader : IInputReader
    {
        private const float JumpHold = 0.2f;

        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        private const string MouseX = "Mouse X";
        private const string MouseY = "Mouse Y";
        private const KeyCode CommandViewpointPresenceSwitch = KeyCode.V;

        private readonly IUpdateService _updateService;

        private float _currentJumpHold;

        public InputReader(IUpdateService updateService)
        {
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
        }

        public event Action ViewpointPresenceSwitchPressed;

        public Vector2 Move { get; private set; }

        public Vector2 Look { get; private set; }

        public bool IsJump { get; private set; }

        public void Enable() =>
            _updateService.Subscribe(Tick, UpdateType.Update);

        public void Disable() =>
            _updateService.Unsubscribe(Tick, UpdateType.Update);

        public void Tick(float deltaTime)
        {
            ReadMovement();
            ReadLook();
            ReadJump(deltaTime);
            ReadCommandViewpointPresenceSwitch();
        }

        private void ReadMovement() =>
            Move = new(Input.GetAxis(HorizontalAxis), Input.GetAxis(VerticalAxis));

        private void ReadLook() =>
            Look = new(Input.GetAxisRaw(MouseX), Input.GetAxisRaw(MouseY));

        private void ReadJump(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _currentJumpHold = JumpHold;
                IsJump = true;

                return;
            }

            if (IsJump)
            {
                _currentJumpHold -= deltaTime;

                if (_currentJumpHold <= 0f)
                {
                    IsJump = false;
                    _currentJumpHold = 0f;
                }
            }
        }

        private void ReadCommandViewpointPresenceSwitch()
        {
            if (Input.GetKeyDown(CommandViewpointPresenceSwitch))
                ViewpointPresenceSwitchPressed?.Invoke();
        }
    }
}