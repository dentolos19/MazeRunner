using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartFunctions : MonoBehaviour
{

    public Slider optionsSensitivity;
    
    private void Start()
    {
        optionsSensitivity.value = Game.Settings.Sensitivity;
    }
    
    public void Play()
    {
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