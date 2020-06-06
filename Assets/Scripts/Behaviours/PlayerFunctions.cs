using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInterface))]
public class PlayerFunctions : MonoBehaviour
{

	private float _defaultSpeed;
	private PlayerMovement _movement;
	private PlayerInterface _interface;

	public GameObject flashlight;

	private void Awake()
	{
		_movement = GetComponent<PlayerMovement>();
		_interface = GetComponent<PlayerInterface>();
		_defaultSpeed = _movement.speed;
	}
	
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
			flashlight.SetActive(!flashlight.activeSelf);
		if (Input.GetKeyDown(KeyCode.LeftShift))
			ToggleSprint(true);
		if (Input.GetKeyUp(KeyCode.LeftShift))
			ToggleSprint(false);
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
			_interface.TogglePauseScreen();
	}

	private void ToggleSprint(bool activate)
	{
		if (activate)
			_movement.speed += 2;
		if (!activate)
			_movement.speed = _defaultSpeed;
	}

}
