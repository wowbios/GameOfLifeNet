using GameOfLife.Abstractions;

namespace GameOfLife.CSharp.Builder
{
    public interface IGameWithSize
    {
        IGameWithRuleset UseRuleset(IRuleset ruleset);

        IGameWithRuleset UseConwaysGameOfLife();
    }
}