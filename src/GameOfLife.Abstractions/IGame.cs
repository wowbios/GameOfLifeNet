namespace GameOfLife.Abstractions
{
    public interface IGame
    {
        void Prepare();
        
        void MakeNextGeneration();
    }
}