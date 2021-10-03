using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    private float _xRotation;
    private CharacterController _characterController;

    public float sensitivity = 100;
    public float speed = 5;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        RotateCamera();
        MovePlayer();
    }

    private void RotateCamera()
    {
        var x = Input.GetAxis("Mouse X") * sensitivity * 10 * Time.deltaTime;
        var y = Input.GetAxis("Mouse Y") * sensitivity * 10 * Time.deltaTime;
        _xRotation -= y;
        _xRotation = Mathf.Clamp(_xRotation, -90, 90);
        Camera.main.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        transform.Rotate(Vector3.up * x);
    }

    private void MovePlayer()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        _characterController.Move((transform.right * x + transform.forward * y) * (speed * Time.deltaTime));
    }

}