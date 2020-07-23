namespace GameOfLifeNet
{
    public interface IGameWithRenderer
    {
        IGameWithField UseBitArrayField();

        IGameWithField UseBoolArrayField();
    }
}