using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuFunctions : MonoBehaviour
{

	[Header("Screens")]
	public GameObject mainScreen;
	public GameObject optionsScreen;
	public GameObject startScreen;
	
	public Slider optionsSensitivity;
	public TMP_Dropdown startDifficulty;

	private void Awake()
	{
		optionsSensitivity.value = Game.Settings.Sensitivity;
	}

	private void DeactiveAllScreens()
	{
		mainScreen.SetActive(false);
		optionsScreen.SetActive(false);
		startScreen.SetActive(false);
	}
	
	public void FnStart()
	{
		DeactiveAllScreens();
		startScreen.SetActive(true);
	}

	public void Play()
	{
		var settings = new MazeWaveSettings();
		switch (startDifficulty.value)
		{
			case 0:
				settings.SetPreset(MazeWaveSettings.PresetDifficulty.Easy);
				break;
			case 1:
				goto default;
			case 2:
				settings.SetPreset(MazeWaveSettings.PresetDifficulty.Hard);
				break;
			case 3:
				settings.SetPreset(MazeWaveSettings.PresetDifficulty.Impossible);
				break;
			default:
				settings.SetPreset(MazeWaveSettings.PresetDifficulty.Normal);
				break;
		}
		MazeGenerator.Settings = settings;
		SceneManager.LoadScene(1);
	}

	public void Options()
	{
		DeactiveAllScreens();
		optionsScreen.SetActive(true);
	}

	public void Exit()
	{
		Application.Quit();
	}

	public void Back()
	{
		Game.Settings.Sensitivity = optionsSensitivity.value;
		Game.Settings.Save();
		DeactiveAllScreens();
		mainScreen.SetActive(true);
	}
	
}