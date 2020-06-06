using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{

	private bool _screenIsUp;
	private Stopwatch _timer;
	
	public TextMeshProUGUI compass;
	public TextMeshProUGUI timer;
	
	[Header("Screens")]
	public GameObject pauseScreen;
	public GameObject deathScreen;
	public GameObject winnerScreen;
	
	private void Start()
	{
		_timer = new Stopwatch();
		_timer.Start();
	}
	
	private void Update()
	{
		var direction = (float)Math.Round(transform.localEulerAngles.y);
		compass.text = $"{direction}º | {GetPoleDirection(direction)}";
		timer.text = _timer.Elapsed.Minutes > 0 ? $"{_timer.Elapsed.Minutes}mins {_timer.Elapsed.Seconds}secs" : $"{_timer.Elapsed.Seconds}secs";
	}

	private void PauseEntireGame(bool activate)
	{
		if (activate)
		{
			Cursor.lockState = CursorLockMode.None;
			_timer.Stop();
			Time.timeScale = 0;
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
			_timer.Start();
			Time.timeScale = 1;
		}
	}
	
	public void TogglePauseScreen()
	{
		if (_screenIsUp && !pauseScreen.activeSelf)
			return;
		pauseScreen.SetActive(!pauseScreen.activeSelf);
		_screenIsUp = pauseScreen.activeSelf;
		PauseEntireGame(_screenIsUp);
	}

	public void ToggleDeathScreen()
	{
		if (_screenIsUp && !deathScreen.activeSelf)
			return;
		deathScreen.SetActive(!deathScreen.activeSelf);
		_screenIsUp = deathScreen.activeSelf;
		PauseEntireGame(_screenIsUp);
	}

	public void ToggleWinnerScreen()
	{
		if (_screenIsUp && !winnerScreen.activeSelf)
			return;
		winnerScreen.SetActive(!winnerScreen.activeSelf);
		_screenIsUp = winnerScreen.activeSelf;
		PauseEntireGame(_screenIsUp);
	}
	
	private static string GetPoleDirection(float value)
	{
		var direction = string.Empty;
		if (value >= 0)
			direction = "North";
		if (value >= 10)
			direction = "North-East";
		if (value >= 80)
			direction = "East";
		if (value >= 100)
			direction = "South-East";
		if (value >= 170)
			direction = "South";
		if (value >= 190)
			direction = "South-West";
		if (value >= 260)
			direction = "West";
		if (value >= 280)
			direction = "North-West";
		if (value >= 350)
			direction = "North";
		return direction;
	}

}