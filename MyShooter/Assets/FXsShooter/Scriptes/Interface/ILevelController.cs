namespace FXs.Shooter
{
    public interface ILevelController : IGameEntity
    {
        Level CurrentLevel { get; }
    }
}
