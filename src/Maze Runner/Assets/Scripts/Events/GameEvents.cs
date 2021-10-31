using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{

    private GameMaster _gameMaster;

    private void Start()
    {
        _gameMaster = FindObjectOfType<GameMaster>();
    }

    public void OnMenu()
    {
        _gameMaster.SetMenuMode(false);
        SceneManager.LoadScene(0);
    }

    public void OnRestart()
    {
        _gameMaster.SetMenuMode(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

}
