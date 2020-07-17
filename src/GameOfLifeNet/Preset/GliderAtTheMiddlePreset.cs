namespace GameOfLifeNet.Preset
{
    public class GliderAtTheMiddlePreset : IPreset
    {
        public void InitializeField(bool[,] field)
        {
            int i = field.GetLength(0) / 2;
            int j = field.GetLength(1) / 2;

            field[i - 1, j] = true;
            field[i, j + 1] = true;
            field[i + 1, j - 1] = true;
            field[i + 1, j] = true;
            field[i + 1, j + 1] = true;
        }
    }
}