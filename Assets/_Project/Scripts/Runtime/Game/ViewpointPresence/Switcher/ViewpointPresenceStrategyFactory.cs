using System;

namespace OnTheMoon.Runtime.Game
{
    public class ViewpointPresenceStrategyFactory : IViewpointPresenceStrategyFactory
    {
        private readonly IViewpointPresenceConfig _config;

        public ViewpointPresenceStrategyFactory(IViewpointPresenceConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public void CreateStrategies(
            ViewpointPresenceType mode,
            ICharacter character,
            ICameraRoot cameraRoot)
        {
            if (character == null)
                throw new ArgumentNullException(nameof(character));

            if (cameraRoot == null)
                throw new ArgumentNullException(nameof(cameraRoot));

            switch (mode)
            {
                case ViewpointPresenceType.FirstPerson:
                    CreateFirstPersonStrategies(character, cameraRoot);
                    break;

                case ViewpointPresenceType.ThirdPerson:
                    CreateThirdPersonStrategies(character, cameraRoot);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        private void CreateFirstPersonStrategies(ICharacter character, ICameraRoot cameraRoot)
        {
            FirstPersonCameraData cameraData = _config.GetFirstPersonCameraData();
            CharacterData characterData = _config.GetCharacterData();

            cameraRoot.SetFollowStrategy(new CameraFirstPersonFollowStrategy(
                cameraRoot.Transform,
                character.Transform,
                cameraData.FollowSpeed));

            cameraRoot.SetRotationStrategy(new FirstPersonRotationStrategy(
                cameraRoot.Transform,
                cameraData.Sensitivity,
                cameraData.MinVerticalAngle,
                cameraData.MaxVerticalAngle));

            character.SetMovementStrategy(new FirstPersonMovementStrategy(
                character.Transform,
                character.Rigidbody,
                character.GravityBody,
                characterData.MovementSpeed));

            character.SetBodyRotationStrategy(new FirstPersonBodyRotationStrategy(
                character.Transform,
                characterData.RotationSpeed));

            character.SetJumpStrategy(new JumpStrategy(
                character.Rigidbody,
                character.GroundChecker,
                characterData.JumpForce));

            UnityEngine.Debug.Log("Включён вид от первого лица");
        }

        private void CreateThirdPersonStrategies(ICharacter character, ICameraRoot cameraRoot)
        {
            ThirdPersonCameraData cameraData = _config.GetThirdPersonCameraData();
            CharacterData characterData = _config.GetCharacterData();

            var thirdPersonCamera = new ThirdPersonCameraStrategy(
                cameraRoot.Transform,
                character.Transform,
                cameraData.Sensitivity,
                cameraData.SmoothSpeed,
                cameraData.MinVerticalAngle,
                cameraData.MaxVerticalAngle,
                cameraData.Distance,
                cameraData.HeightOffset);

            cameraRoot.SetFollowStrategy(thirdPersonCamera);
            cameraRoot.SetRotationStrategy(thirdPersonCamera);

            character.SetMovementStrategy(new ThirdPersonMovementStrategy(
                character.Transform,
                character.Rigidbody,
                character.GravityBody,
                characterData.MovementSpeed,
                characterData.RotationSpeed,
                cameraRoot.Transform));

            character.ResetBodyRotationStrategy();

            character.SetJumpStrategy(new JumpStrategy(
                character.Rigidbody,
                character.GroundChecker,
                characterData.JumpForce));

            UnityEngine.Debug.Log("Включён вид от третьего лица");
        }
    }
}