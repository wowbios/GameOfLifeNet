namespace GameOfLifeNet.Preset
{
    public class GliderAtTheMiddlePreset : IPreset
    {
        public void InitializeField(IGameField field)
        {
            int i = field.Width / 2;
            int j = field.Height / 2;

            field[i - 1, j] = true;
            field[i, j + 1] = true;
            field[i + 1, j - 1] = true;
            field[i + 1, j] = true;
            field[i + 1, j + 1] = true;
        }
    }
}