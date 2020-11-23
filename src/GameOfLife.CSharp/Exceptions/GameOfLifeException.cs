using System;

namespace GameOfLife.CSharp.Exceptions
{
    public class GameOfLifeException : Exception
    {
        public GameOfLifeException(string message)
            : base(message)
        {
        }
    }
}