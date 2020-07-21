namespace GameOfLifeNet
{
    public interface IRuleset
    {
        bool IsAlive(bool[,] field, int x, int y);
    }
}