public class MazeWaveSettings
{

	public enum PresetDifficulty { Easy, Normal, Hard, Impossible }
	
	public int Size { get; set; } = 25;
	public int EnemyAmount { get; set; } = 5;
	public bool EnableBoss { get; set; } = true;

	public void SetPreset(PresetDifficulty preset)
	{
		switch (preset)
		{
			case PresetDifficulty.Easy:
				Size = 45;
				EnemyAmount = 5;
				EnableBoss = false;
				break;
			case PresetDifficulty.Normal:
				goto default;
			case PresetDifficulty.Hard:
				Size = 65;
				EnemyAmount = 25;
				EnableBoss = false;
				break;
			case PresetDifficulty.Impossible:
				Size = 75;
				EnemyAmount = 35;
				EnableBoss = true;
				break;
			default:
				Size = 55;
				EnemyAmount = 15;
				EnableBoss = false;
				break;
		}
	}
	
}
