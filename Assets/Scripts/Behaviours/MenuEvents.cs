using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuEvents : MonoBehaviour
{

    [Header("Menu Views")]
    public GameObject mainView;
    public GameObject playView;
    public GameObject optionsView;

    [Header("Menu Objects")]
    public Slider sensitivitySlider;
    public Slider volumeSlider;
    public TMP_Dropdown difficultyDropdown;

    private void Start()
    {
        IntersceneSoundEmitter.Instance.UpdateVolume(Game.Settings.Volume);
        sensitivitySlider.value = Game.Settings.Sensitivity;
        volumeSlider.value = Game.Settings.Volume;
    }

    public void PlayGame()
    {
        var settings = new MazeWaveSettings();
        switch (difficultyDropdown.value)
        {
            case 0:
                settings.SetPreset(MazeWaveSettings.PresetDifficulty.Easy);
                break;
            case 1:
                settings.SetPreset(MazeWaveSettings.PresetDifficulty.Normal);
                break;
            case 2:
                settings.SetPreset(MazeWaveSettings.PresetDifficulty.Hard);
                break;
            case 3:
                settings.SetPreset(MazeWaveSettings.PresetDifficulty.Impossible);
                break;
        }
        GameMaster.Settings = settings;
        SceneManager.LoadScene(1);
    }

    public void ShowPlayOptions()
    {
        mainView.SetActive(false);
        playView.SetActive(true);
        optionsView.SetActive(false);
    }

    public void ShowMainView()
    {
        mainView.SetActive(true);
        playView.SetActive(false);
        optionsView.SetActive(false);
    }
    
    public void LoadOptions()
    {
        mainView.SetActive(false);
        playView.SetActive(false);
        optionsView.SetActive(true);
    }

    public void SaveOptions()
    {
        Game.Settings.Sensitivity = sensitivitySlider.value;
        Game.Settings.Volume = volumeSlider.value;
        Game.Settings.Save();
        mainView.SetActive(true);
        playView.SetActive(false);
        optionsView.SetActive(false);
    }
    public void UpdateVolume()
    {
        IntersceneSoundEmitter.Instance.UpdateVolume(volumeSlider.value);
    }
    
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Triggered application quit.");
    }
    
}