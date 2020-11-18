using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuEvents : MonoBehaviour
{

    [Header("Menu Views")]
    public GameObject mainView;
    public GameObject optionsView;

    [Header("Menu Objects")]
    public Slider sensitivitySlider;

    public Slider volumeSlider;

    private void Start()
    {
        IntersceneSoundEmitter.Instance.UpdateVolume(Game.Settings.Volume);
        sensitivitySlider.value = Game.Settings.Sensitivity;
        volumeSlider.value = Game.Settings.Volume;
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
        Debug.Log("Triggered application quit.");
    }

    public void LoadOptions()
    {
        mainView.SetActive(false);
        optionsView.SetActive(true);
    }

    public void SaveOptions()
    {
        Game.Settings.Sensitivity = sensitivitySlider.value;
        Game.Settings.Volume = volumeSlider.value;
        Game.Settings.Save();
        mainView.SetActive(true);
        optionsView.SetActive(false);
    }
    public void UpdateVolume()
    {
        IntersceneSoundEmitter.Instance.UpdateVolume(volumeSlider.value);
    }
    
}