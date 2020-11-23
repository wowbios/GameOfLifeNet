using GameOfLife.Abstractions;

namespace GameOfLife.CSharp.Builder
{
    public interface IGameWithRenderer
    {
        IGameWithPreset WithPreset(IPreset preset);

        IGameWithPreset WithRandomPreset(int fulfillPercent);

        IGameWithPreset WithGlider();
    }
}