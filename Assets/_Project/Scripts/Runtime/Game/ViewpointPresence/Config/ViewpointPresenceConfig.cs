using UnityEngine;

namespace OnTheMoon.Runtime.Game
{
    [CreateAssetMenu(
        fileName = nameof(ViewpointPresenceConfig),
        menuName = Constants.AssetMenuConfigPath + nameof(ViewpointPresenceConfig),
        order = 0)]
    public class ViewpointPresenceConfig : ScriptableObject, IViewpointPresenceConfig
    {
        [SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private FirstPersonCameraConfig _firstPersonCameraConfig;
        [SerializeField] private ThirdPersonCameraConfig _thirdPersonCameraConfig;

        public CharacterData GetCharacterData() =>
            _characterConfig.GetData();

        public FirstPersonCameraData GetFirstPersonCameraData() =>
            _firstPersonCameraConfig.GetData();

        public ThirdPersonCameraData GetThirdPersonCameraData() =>
            _thirdPersonCameraConfig.GetData();
    }
}