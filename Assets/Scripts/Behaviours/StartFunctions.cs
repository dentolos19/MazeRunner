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
        var settings = new WaveSettings();
        switch (startDifficulty.value)
        {
            case 0:
                settings.SetPreset(WaveSettings.PresetDifficulty.Easy);
                break;
            case 1:
                settings.SetPreset(WaveSettings.PresetDifficulty.Normal);
                break;
            case 2:
                settings.SetPreset(WaveSettings.PresetDifficulty.Hard);
                break;
            case 3:
                settings.SetPreset(WaveSettings.PresetDifficulty.Impossible);
                break;
        }
        MazeGenerator.Settings = settings;
        SceneManager.LoadScene("Maze");
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