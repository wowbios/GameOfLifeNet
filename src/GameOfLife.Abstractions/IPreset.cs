namespace GameOfLife.Abstractions
{
    public interface IPreset
    {
        void InitializeField(bool[,] field);
    }
}