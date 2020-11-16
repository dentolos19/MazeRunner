using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceEvents : MonoBehaviour
{

    [Header("Menu Views")]
    public GameObject mainView;
    public GameObject optionsView;

    [Header("Menu Objects")]
    public Slider sensitivitySlider;

    public Slider volumeSlider;

    private void Start()
    {
        IntersceneSoundEmitter.Instance.source.volume = Game.Settings.Volume;
    }
    
    public void PlayGame()
    {
        // var settings = new MazeWaveSettings();
        // settings.SetPreset(MazeWaveSettings.PresetDifficulty.Impossible);
        // GameMaster.Settings = settings;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Triggered quit.");
    }

    public void LoadOptions()
    {
        sensitivitySlider.value = Game.Settings.Sensitivity;
        volumeSlider.value = Game.Settings.Volume;
        mainView.SetActive(false);
        optionsView.SetActive(true);
    }

    public void SaveOptions()
    {
        Game.Settings.Sensitivity = sensitivitySlider.value;
        Game.Settings.Volume = volumeSlider.value;
        Game.Settings.Save();
        IntersceneSoundEmitter.Instance.source.volume = Game.Settings.Volume;
        mainView.SetActive(true);
        optionsView.SetActive(false);
    }
    
}