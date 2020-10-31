public class MazeWaveSettings
{

    public enum PresetDifficulty { Easy, Normal, Hard, Impossible }
	
    public int MazeSize { get; private set; } = 25;
    public int EnemyAmount { get; private set; } = 5;
    public bool EnableBoss { get; private set; } = true;

    public void SetPreset(PresetDifficulty preset)
    {
        switch (preset)
        {
            case PresetDifficulty.Easy:
                MazeSize = 45;
                EnemyAmount = 5;
                EnableBoss = false;
                break;
            case PresetDifficulty.Normal:
                goto default;
            case PresetDifficulty.Hard:
                MazeSize = 65;
                EnemyAmount = 25;
                EnableBoss = false;
                break;
            case PresetDifficulty.Impossible:
                MazeSize = 75;
                EnemyAmount = 35;
                EnableBoss = true;
                break;
            default:
                MazeSize = 55;
                EnemyAmount = 15;
                EnableBoss = false;
                break;
        }
    }
	
}