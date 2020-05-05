using System;
using TMPro;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{

    private SpeedTimer _timer;
    
    public TextMeshProUGUI compass;
    public TextMeshProUGUI timer;

    private void Awake()
    {
        _timer = gameObject.AddComponent<SpeedTimer>();
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
        timer.text = _timer.Elasped.Minutes > 0 ? $"{_timer.Elasped.Minutes} mins {_timer.Elasped.Seconds} secs" : $"{_timer.Elasped.Seconds} secs";
    }

    private static string GetDirectionFromDouble(double value)
    {
        var direction = string.Empty;
        if (value >= 0)
        {
            direction = "North";
        }
        if (value >= 10)
        {
            direction = "North-East";
        }
        if (value >= 80)
        {
            direction = "East";
        }
        if (value >= 100)
        {
            direction = "South-East";
        }
        if (value >= 170)
        {
            direction = "South";
        }
        if (value >= 190)
        {
            direction = "South-West";
        }
        if (value >= 260)
        {
            direction = "West";
        }
        if (value >= 280)
        {
            direction = "North-West";
        }
		if (value >= 350)
		{
			direction = "North";
		}
        return direction;
    }

}