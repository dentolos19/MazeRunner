using UnityEngine;

public class GameMaster : MonoBehaviour
{

    private bool _isMenuActive;
    private float _initialTimeScale;

    [Header("Script Prerequisites")]

    public GameObject pauseMenu;
    public GameObject deathMenu;
    public GameObject finishMenu;

    private void Start()
    {
        _initialTimeScale = Time.timeScale;
    }

    public void SetMenuMode(bool state)
    {
        Time.timeScale = state
            ? 0 // pauses game
            : _initialTimeScale; // restores game
        Cursor.lockState = state
            ? CursorLockMode.None // unlock cursor
            : CursorLockMode.Locked; // lock cursor
        _isMenuActive = state;
    }

    public void TogglePauseMenu()
    {
        if (_isMenuActive)
            return;
        var newState = !pauseMenu.activeSelf;
        pauseMenu.SetActive(newState);
        SetMenuMode(newState);
    }

    public void EndGame(bool isFinish)
    {
        if (isFinish)
            finishMenu.SetActive(true); // ends game by finishing
        else
            deathMenu.SetActive(true); // ends game by dying
        SetMenuMode(true);
    }

}