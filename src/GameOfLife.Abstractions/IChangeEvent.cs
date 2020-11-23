namespace GameOfLife.Abstractions
{
    public interface IChangeEvent
    {
        int X { get; }
        
        int Y { get; }
        
        bool IsAlive { get; }
    }
}