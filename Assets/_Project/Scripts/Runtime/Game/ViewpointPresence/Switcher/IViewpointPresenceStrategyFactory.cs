namespace OnTheMoon.Runtime.Game
{
    public interface IViewpointPresenceStrategyFactory
    {
        void CreateStrategies(
            ViewpointPresenceType mode,
            ICharacter character,
            ICameraRoot cameraRoot);
    }
}