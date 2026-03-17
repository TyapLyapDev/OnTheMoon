namespace OnTheMoon.Runtime.Game
{
    public interface IPlayerBuilder
    {
        IPlayerBuilder WithName(string name);

        IPlayer Build();
    }
}