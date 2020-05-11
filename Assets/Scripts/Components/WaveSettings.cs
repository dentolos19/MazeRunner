public class WaveSettings
{
    
    public enum PresetDifficulty { Easy, Normal, Hard, Impossible }

    public int MazeSize { get; private set; } = 10;
    public int EnemyAmount { get; private set; } = 63;
    public bool EnableBoss { get; private set; } = true;
    
    public void SetPreset(PresetDifficulty preset)
    {
        switch (preset)
        {
            case PresetDifficulty.Easy:
                MazeSize = 15;
                EnemyAmount = 5;
                EnableBoss = false;
                break;
            case PresetDifficulty.Normal:
                goto default;
            case PresetDifficulty.Hard:
                MazeSize = 31;
                EnemyAmount = 20;
                EnableBoss = false;
                break;
            case PresetDifficulty.Impossible:
                MazeSize = 41;
                EnemyAmount = 25;
                EnableBoss = true;
                break;
            default:
                MazeSize = 21;
                EnemyAmount = 10;
                EnableBoss = false;
                break;
        }
    }

}