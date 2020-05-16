using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartFunctions : MonoBehaviour
{

	public Slider optionsSensitivity;
	public TMP_Dropdown startDifficulty;

	private void Start()
	{
		optionsSensitivity.value = Game.Settings.Sensitivity;
	}

	public void Play()
	{
		var settings = new MazeSettings();
		switch (startDifficulty.value)
		{
			case 0:
				settings.SetPreset(MazeSettings.PresetDifficulty.Easy);
				break;
			case 1:
				settings.SetPreset(MazeSettings.PresetDifficulty.Normal);
				break;
			case 2:
				settings.SetPreset(MazeSettings.PresetDifficulty.Hard);
				break;
			case 3:
				settings.SetPreset(MazeSettings.PresetDifficulty.Impossible);
				break;
		}
		MazeGenerator.Settings = settings;
		SceneManager.LoadScene(1);
	}

	public void Exit()
	{
		Application.Quit();
	}

	public void Save()
	{
		Game.Settings.Sensitivity = optionsSensitivity.value;
		Game.Settings.Save();
	}

}