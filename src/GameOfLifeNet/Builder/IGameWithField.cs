namespace GameOfLifeNet
{
    public interface IGameWithField
    {
        IGameWithPreset WithPreset(IPreset preset);

        IGameWithPreset WithRandomPreset(int fulfillPercent);

        IGameWithPreset WithGlider();
    }
}