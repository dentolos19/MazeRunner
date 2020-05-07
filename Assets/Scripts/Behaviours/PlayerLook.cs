using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    private float _senstivity;
    private float _xRotation;
    
    public Transform body;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _senstivity = Game.Settings.Sensitivity * 10;
    }

    private void Update()
    {
        var x = Input.GetAxis("Mouse X") * _senstivity * Time.deltaTime;
        var y = Input.GetAxis("Mouse Y") * _senstivity * Time.deltaTime;
        _xRotation -= y;
        _xRotation = Mathf.Clamp(_xRotation, -90, 90);
        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        body.Rotate(Vector3.up * x);
    }
    
}