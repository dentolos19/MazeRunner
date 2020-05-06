using UnityEngine;
using UnityEngine.SceneManagement;

public class StartFunctions : MonoBehaviour
{

    public void Play()
    {
        SceneManager.LoadScene("Maze");
    }

    public void Exit()
    {
        Application.Quit();
    }
    
}