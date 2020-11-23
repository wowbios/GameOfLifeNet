namespace GameOfLife.Abstractions
{
    public interface IRuleset
    {
        bool IsAlive(bool[,] field, int x, int y);
    }
}