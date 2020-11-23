namespace GameOfLife.CSharp
{
    public interface IGame
    {
        void Prepare();
        void MakeNextGeneration();
    }
}