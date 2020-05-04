using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    private bool _isFlashlightActive;
    
    public GameObject flashlight;

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
            ToggleFlashlight();
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

}