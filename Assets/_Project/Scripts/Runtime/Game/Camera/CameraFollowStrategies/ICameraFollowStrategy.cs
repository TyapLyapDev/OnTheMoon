namespace OnTheMoon.Runtime.Game
{
    public interface ICameraFollowStrategy
    {
        void Follow(float deltaTime);
    }
}