using UnityEngine;

public class GameMaster : MonoBehaviour
{

    private bool _isMenuOn;
    private float _initialTimeScale;

    [Header("Script Prerequisites")]

    public GameObject pauseMenu;
    public GameObject deathMenu;
    public GameObject finishMenu;

    private void Start()
    {
        _initialTimeScale = Time.timeScale;
    }

    public void SetMenuMode(bool isOn)
    {
        Time.timeScale = isOn ? 0 : _initialTimeScale;
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