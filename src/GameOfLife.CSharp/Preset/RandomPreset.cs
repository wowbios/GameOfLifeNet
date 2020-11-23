using System;
using GameOfLife.Abstractions;

namespace GameOfLife.CSharp.Preset
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
        
        public void InitializeField(bool[,] field)
        {
            int width = field.GetLength(0);
            int height = field.GetLength(1);
            
            var random = new Random();
            int totalCount = width * height * _fulFillPercent / 100;
            for (var i = 0; i < totalCount; i++)
            {
                int x = random.Next(0, width);
                int y = random.Next(0, height);
                field[x, y] = true;
            }
        }
    }
}