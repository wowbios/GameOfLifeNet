using GameOfLife.Abstractions;
using System;

namespace GameOfLife.CSharp.Ruleset
{
    public class RandomRevivalRulesetDecorator : IRuleset
    {
        private readonly IRuleset _baseRuleset;
        private readonly Random _random = new Random();
        private int _unchangedGenerations = 0;
        private const int MaxUnchangedGenerations = 10;

        public RandomRevivalRulesetDecorator(IRuleset baseRuleset)
        {
            _baseRuleset = baseRuleset;
        }

        public bool IsAlive(bool[,] field, int x, int y)
        {
            bool result = _baseRuleset.IsAlive(field, x, y);
            
            if (x == 0 && y == 0)
            {
                bool changed = false;
                for (int i = 0; i < field.GetLength(0); i++)
                {
                    for (int j = 0; j < field.GetLength(1); j++)
                    {
                        if (_baseRuleset.IsAlive(field, i, j) != field[i,j])
                        {
                            changed = true;
                            break;
                        }
                    }
                    if (changed) break;
                }

                if (!changed)
                {
                    _unchangedGenerations++;
                    if (_unchangedGenerations >= MaxUnchangedGenerations)
                    {
                        _unchangedGenerations = 0;
                        return _random.NextDouble() < 0.05;
                    }
                }
                else
                {
                    _unchangedGenerations = 0;
                }
            }

            // Only apply random revival to non-pattern cells
            if (result != field[x,y] && result && 
                PatternDetector.IsKnownPattern(field, x, y))
            {
                return field[x,y]; // Keep original state for known patterns
            }

            return result;
        }
    }
}
