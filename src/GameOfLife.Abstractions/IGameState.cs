namespace GameOfLife.Abstractions
{
    public interface IGameState
    {
        long Generation { get; }
        
        IChangeEvent[] Events { get; }
    }
}