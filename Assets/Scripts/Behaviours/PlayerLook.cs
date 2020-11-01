using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    private float _xRotation;
    private float _sensitivity;
    
    public Transform playerObject;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _sensitivity = Game.Settings.Sensitivity * 10;
    }

    private void Update()
    {
        var mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90, 90);
        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        playerObject.Rotate(Vector3.up * mouseX);
    }
    
}