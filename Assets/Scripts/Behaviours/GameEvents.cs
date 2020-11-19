using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{

    public static GameEvents Instance { get; private set; }

    private float _defaultTimeScale;
    
    [Header("Game Views")]
    public GameObject viewBackground;
    public GameObject winView;
    public GameObject loseView;
    public GameObject pauseView;

    private void Awake()
    {
        _defaultTimeScale = Time.timeScale;
    }

    private void Start()
    {
        Instance = this;
    }
    
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
            TogglePauseView();
    }

    private void CloseAnyView()
    {
        viewBackground.SetActive(false);
        winView.SetActive(false);
        loseView.SetActive(false);
        pauseView.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = _defaultTimeScale;
    }
    
    public void TogglePauseView()
    {
        CloseAnyView();
        pauseView.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public void ToggleWinView()
    {
        CloseAnyView();
        winView.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public void ToggleLoseView()
    {
        CloseAnyView();
        loseView.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public void BackToMenu()
    {
        CloseAnyView();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }

}