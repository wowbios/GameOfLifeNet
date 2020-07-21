using System;

namespace GameOfLifeNet.Exceptions
{
    public class GameOfLifeException : Exception
    {
        public GameOfLifeException(string message)
            : base(message)
        {
        }
    }
}