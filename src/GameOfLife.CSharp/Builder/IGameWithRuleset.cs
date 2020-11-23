using GameOfLife.Abstractions;

namespace GameOfLife.CSharp.Builder
{
    public interface IGameWithRuleset
    {
        IGameWithRenderer RenderWith(IRender render);
    }
}