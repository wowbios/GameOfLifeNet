using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife.CSharp.Ruleset
{
    public static class PatternDetector
    {
        private static readonly HashSet<string> StablePatternHashes = new HashSet<string>
        {
            // Common still lifes (block, beehive, loaf, boat, tub)
            "4x4_1111000000000000",
            "6x5_01100010100010000110000000000",
            "6x6_001100010010100001001100000000",
            "5x5_0100010100100010000000000",
            "5x5_0010001010001000000000000",
            
            // Common oscillators (blinker, toad, beacon, pulsar)
            "3x3_111000000",
            "6x6_000111111000000000",
            "6x6_110011000011110000",
            "15x15_000000111000000000000111000000000000000000000111000000000000111000000000000000000000111000000000000111000000000000000000000111000000000000111000000"
        };

        public static bool IsKnownPattern(bool[,] field, int x, int y, int size = 5)
        {
            // Check if the pattern around (x,y) matches any known stable pattern
            string patternHash = GetPatternHash(field, x, y, size);
            return StablePatternHashes.Contains(patternHash);
        }

        private static string GetPatternHash(bool[,] field, int x, int y, int size)
        {
            var pattern = new StringBuilder();
            int halfSize = size / 2;
            int width = field.GetLength(0);
            int height = field.GetLength(1);

            pattern.Append($"{size}x{size}_");

            for (int dy = -halfSize; dy <= halfSize; dy++)
            {
                for (int dx = -halfSize; dx <= halfSize; dx++)
                {
                    int nx = (x + dx + width) % width;
                    int ny = (y + dy + height) % height;
                    pattern.Append(field[nx, ny] ? '1' : '0');
                }
            }

            return pattern.ToString();
        }
    }
}
