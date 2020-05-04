using UnityEngine;

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