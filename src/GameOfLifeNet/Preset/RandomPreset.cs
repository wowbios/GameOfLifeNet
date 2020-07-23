using System;

namespace GameOfLifeNet.Preset
{
    public class RandomPreset : IPreset
    {
        private readonly int _fulFillPercent;

        public RandomPreset(int fulFillPercent)
        {
            if(fulFillPercent < 0 || fulFillPercent > 100)
                throw new ArgumentException("Fulfill must be positive and less than 100", nameof(fulFillPercent));
            
            _fulFillPercent = fulFillPercent;
        }
        
        public void InitializeField(IGameField field)
        {
            var random = new Random();
            int totalCount = field.Width * field.Height * _fulFillPercent / 100;
            for (var i = 0; i < totalCount; i++)
            {
                int x = random.Next(0, field.Width);
                int y = random.Next(0, field.Height);
                field[x, y] = true;
            }
        }
    }
}