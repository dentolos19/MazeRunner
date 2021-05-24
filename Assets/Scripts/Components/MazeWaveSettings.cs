using UnityEngine;

public class MazeWaveSettings
{

    public enum PresetDifficulty { Easy, Normal, Hard, Impossible, Debug }

    public PresetDifficulty Difficulty { get; private set; } = PresetDifficulty.Debug;
    public int MazeSize { get; private set; } = 25;
    public int EnemyAmount { get; private set; } = 5;
    public int EnemyDistance { get; private set; } = 10;
    public bool EnableBoss { get; private set; } = true;
    public int BossDistance { get; private set; } = 20;

    public void SetPreset(PresetDifficulty preset)
    {
        switch (preset)
        {
            case PresetDifficulty.Easy:
                MazeSize = 45;
                EnemyAmount = 5;
                // EnemyDistance = 10;
                EnableBoss = false;
                // BossDistance = 20;
                break;
            case PresetDifficulty.Normal:
                goto default;
            case PresetDifficulty.Hard:
                MazeSize = 65;
                EnemyAmount = 25;
                // EnemyDistance = 10;
                EnableBoss = false;
                // BossDistance = 20;
                break;
            case PresetDifficulty.Impossible:
                MazeSize = 75;
                EnemyAmount = 35;
                // EnemyDistance = 10;
                EnableBoss = true;
                // BossDistance = 20;
                break;
            default:
                MazeSize = 55;
                EnemyAmount = 15;
                // EnemyDistance = 10;
                EnableBoss = false;
                // BossDistance = 20;
                break;
        }
        Difficulty = preset;
    }
	
}