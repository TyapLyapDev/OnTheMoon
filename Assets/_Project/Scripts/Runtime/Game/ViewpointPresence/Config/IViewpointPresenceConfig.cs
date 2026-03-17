namespace OnTheMoon.Runtime.Game
{
    public interface IViewpointPresenceConfig
    {
        CharacterData GetCharacterData();

        FirstPersonCameraData GetFirstPersonCameraData();

        ThirdPersonCameraData GetThirdPersonCameraData();
    }
}