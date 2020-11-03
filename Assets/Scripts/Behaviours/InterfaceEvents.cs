using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceEvents : MonoBehaviour
{

    public void OnPlayClick()
    {
        // var settings = new MazeWaveSettings();
        // settings.SetPreset(MazeWaveSettings.PresetDifficulty.Impossible);
        // GameMaster.Settings = settings;
        SceneManager.LoadScene(1);
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
    
}