namespace GameOfLifeNet
{
    public interface IGameWithRenderer
    {
        IGameWithPreset WithPreset(IPreset preset);

        IGameWithPreset WithRandomPreset(int fulfillPercent);

        IGameWithPreset WithGlider();
    }
}