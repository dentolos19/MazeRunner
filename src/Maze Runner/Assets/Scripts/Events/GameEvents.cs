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
        // TODO: back to menu scene
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
