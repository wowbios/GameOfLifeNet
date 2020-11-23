using GameOfLife.Abstractions;

namespace GameOfLife.CSharp.Ruleset
{
    public class ConwaysRuleset : IRuleset
    {
        public bool IsAlive(bool[,] field, int x, int y)
        {
            int width = field.GetLength(0);
            int height = field.GetLength(1);

            byte aliveNeighbor = GetClosestAliveCount(field, width, height, x, y);
            bool result = field[x, y];
            if (result)
            {
                if (aliveNeighbor != 2 && aliveNeighbor != 3)
                    return false;
            }
            else
            {
                if (aliveNeighbor == 3)
                    return true;
            }

            return result;
        }

        private static byte GetClosestAliveCount(bool[,] field, int width, int height, int x, int y)
        {
            //    1_2_3
            // 1| 1 2 3
            // 2| 4 5 6
            // 3| 7 8 9
            byte alive = 0;

            int i1 = (x + width - 1) % width;
            int i3 = (x + width + 1) % width;
            int j1 = (y + height - 1) % height;
            int j3 = (y + height + 1) % height;

            Check(i1, j1);
            Check(i1, y);
            Check(i1, j3);

            Check(x, j1);
            Check(x, j3);

            Check(i3, j1);
            Check(i3, y);
            Check(i3, j3);

            return alive;

            void Check(int xx, int yy)
            {
                if (field[xx, yy]) alive++;
            }
        }
    }
}