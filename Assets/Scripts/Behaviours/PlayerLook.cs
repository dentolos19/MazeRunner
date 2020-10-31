using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    private float _xRotation;
    
    public Transform playerObject;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        var mouseX = Input.GetAxis("Mouse X") * (Game.Settings.Sensitivity * 10) * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * (Game.Settings.Sensitivity * 10) * Time.deltaTime;
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90, 90);
        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        playerObject.Rotate(Vector3.up * mouseX);
    }
    
}