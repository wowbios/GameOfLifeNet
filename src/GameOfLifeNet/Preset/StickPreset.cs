namespace GameOfLifeNet.Preset
{
    public class StickPreset : IPreset
    {
        public void InitializeField(bool[,] field)
        {
            field[1, 2] = true;
            field[2, 2] = true;
            field[3, 2] = true;
        }
    }
}