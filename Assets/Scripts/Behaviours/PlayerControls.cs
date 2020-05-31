using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
<<<<<<< HEAD
[RequireComponent(typeof(PlayerInterface))]
public class PlayerControls : MonoBehaviour
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
=======
public class PlayerControls : MonoBehaviour
{

    private bool _isFlashlightActive = true;
    private PlayerMovement _movement;

    public GameObject flashlight;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            ToggleFlashlight();
        if (Input.GetKeyDown(KeyCode.LeftControl))
            ToggleSprint(true);
        if (Input.GetKeyUp(KeyCode.LeftControl))
            ToggleSprint(false);
    }

    private void ToggleFlashlight()
    {
        if (_isFlashlightActive)
        {
            flashlight.SetActive(false);
            _isFlashlightActive = false;
        }
        else
        {
            flashlight.SetActive(true);
            _isFlashlightActive = true;
        }
    }

    private void ToggleSprint(bool active)
    {
        _movement.speed = active ? 10 : 5;
    }

}
>>>>>>> RunAwayHaroldOld/master-old
