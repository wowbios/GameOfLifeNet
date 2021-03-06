﻿using System.Drawing;

namespace GameOfLife.CSharp.Builder
{
    public interface IGameBuilder
    {
        IGameWithSize SetSize(int width, int height);

        IGameWithSize SetSize(int size);

        IGameWithSize SetSize(Size size);
    }
}