namespace GameOfLifeNet
{
    public interface IGameWithRuleset
    {
        IGameWithRenderer RenderWith(IRender render);
    }
}