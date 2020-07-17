using System;

namespace GameOfLifeNet
{
    public class GameSettings
    {
        public int Width { get; }
        
        public int Height { get; }

        public IPreset Preset { get; }
        
        public GameSettings(int width, int height, IPreset preset)
        {
            if(width <= 0)
                throw new ArgumentException("Width must be positive", nameof(width));
            
            if(height <= 0)
                throw new ArgumentException("Height must be positive", nameof(height));
            
            Width = width;
            Height = height;
            Preset = preset ?? throw new ArgumentNullException(nameof(preset));
        }
        
        public GameSettings(int size, IPreset preset)
            : this(size, size, preset)
        {
            
        }
    }
}