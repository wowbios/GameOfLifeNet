namespace GameOfLifeNet
{
    public interface IGameWithSize
    {
        IGameWithRuleset UseRuleset(IRuleset ruleset);

        IGameWithRuleset UseConwaysGameOfLife();
    }
}