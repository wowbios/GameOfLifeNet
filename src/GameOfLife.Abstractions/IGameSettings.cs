namespace GameOfLife.Abstractions
{
    public interface IGameSettings
    {
         int Width { get; }

         int Height { get; }

         IPreset Preset { get; }

         IRender Render { get; }

         IRuleset Ruleset { get; }
    }
}