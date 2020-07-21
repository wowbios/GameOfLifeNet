using System;

namespace GameOfLifeNet
{
    internal interface IGameSettings
    {
         int Width { get; }

         int Height { get; }

         IPreset Preset { get; }

         IRender Render { get; }

         IRuleset Ruleset { get; }
    }
}