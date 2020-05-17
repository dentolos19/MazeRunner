using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{

	private bool _gameHasEnded;

	private bool _isPauseScreenShowing;
	private Stopwatch _timer;

	public TextMeshProUGUI compass;

	public GameObject endScreen;
	public GameObject pauseScreen;
	public GameObject deathScreen;
	public TextMeshProUGUI timer;

	private void Awake()
	{
		_timer = new Stopwatch();
	}

	private void Start()
	{
		_timer.Start();
	}

	private void Update()
	{
		var value = Math.Round(transform.localEulerAngles.y);
		var direction = GetDirectionFromDouble(value);
		compass.text = $"{value}º | {direction}";
		timer.text = _timer.Elapsed.Minutes > 0 ? $"{_timer.Elapsed.Minutes}mins {_timer.Elapsed.Seconds}secs" : $"{_timer.Elapsed.Seconds}secs";
		if (Input.GetKeyDown(KeyCode.Tab))
			TogglePause();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Goal"))
			return;
		Cursor.lockState = CursorLockMode.None;
		_timer.Stop();
		_gameHasEnded = true;
		endScreen.SetActive(true);
	}

	private void TogglePause()
	{
		if (_gameHasEnded)
			return;
		_isPauseScreenShowing = !_isPauseScreenShowing;
		pauseScreen.SetActive(_isPauseScreenShowing);
		if (_isPauseScreenShowing)
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

	public void ShowDeathScreen()
	{
		Cursor.lockState = CursorLockMode.None;
		_timer.Stop();
		_gameHasEnded = true;
		deathScreen.SetActive(true);
	}

	private static string GetDirectionFromDouble(double value)
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