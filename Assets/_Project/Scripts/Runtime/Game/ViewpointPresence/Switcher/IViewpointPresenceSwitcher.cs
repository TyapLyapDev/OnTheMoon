namespace OnTheMoon.Runtime.Game
{
    public interface IViewpointPresenceSwitcher
    {
        void SetCharacter(ICharacter character);

        void SetMode(ViewpointPresenceType viewpointPresenceType);

        void Enable();

        void Disable();
    }
}