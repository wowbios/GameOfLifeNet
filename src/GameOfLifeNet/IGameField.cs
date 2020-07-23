namespace GameOfLifeNet
{
    public interface IGameField
    {
        int Width { get; }

        int Height { get; }

        bool this[int x, int y] { get; set; }

        void Swap();
    }
}