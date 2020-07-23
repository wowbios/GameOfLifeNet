namespace GameOfLifeNet
{
    public interface IRuleset
    {
        bool IsAlive(IGameField field, int x, int y);
    }
}