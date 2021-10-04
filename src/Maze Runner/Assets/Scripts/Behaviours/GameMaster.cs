using UnityEngine;

public class GameMaster : MonoBehaviour
{

    private bool _isMenuOn;

    [Header("Script Prequisites")]

    public GameObject pauseMenu;
    public GameObject deathMenu;
    public GameObject finishMenu;

    private void SetMenuMode(bool isOn)
    {
        Time.timeScale = isOn ? 0 : Game.InitialTimeScale;
        Cursor.lockState = isOn ? CursorLockMode.None : CursorLockMode.Locked;
        _isMenuOn = isOn;
    }

    public void TogglePauseMenu()
    {
        if (_isMenuOn)
            return;
        var currentState = !pauseMenu.activeSelf;
        pauseMenu.SetActive(currentState);
        SetMenuMode(currentState);
    }

    public void EndGame(bool isFinish)
    {
        if (isFinish)
            finishMenu.SetActive(true);
        else
            deathMenu.SetActive(true);
        SetMenuMode(true);
    }

}