namespace GameOfLifeNet.Ruleset
{
    public class ConwaysRuleset : IRuleset
    {
        public bool IsAlive(IGameField field, int x, int y)
        {
            byte aliveNeighbor = GetClosestAliveCount(field, x, y);
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

        private static byte GetClosestAliveCount(IGameField field, int x, int y)
        {
            //    1_2_3
            // 1| 1 2 3
            // 2| 4 5 6
            // 3| 7 8 9
            byte alive = 0;

            int i1 = (x + field.Width - 1) % field.Width;
            int i3 = (x + field.Width + 1) % field.Width;
            int j1 = (y + field.Height - 1) % field.Height;
            int j3 = (y + field.Height + 1) % field.Height;

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