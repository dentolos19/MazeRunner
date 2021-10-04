using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    private float _xRotation;

    [Header("Script Prerequisites")]

    public Transform playerCamera;
    public CharacterController _playerController;

    [Header("Script Settings")]

    public float rotationSensitivity = 100;
    public float movementSpeed = 5;

    [Header("")]

    public GameObject flashlightObject;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        RotateCamera();
        MovePlayer();
        ManageInputs();
    }

    private void RotateCamera()
    {
        var x = Input.GetAxis("Mouse X") * rotationSensitivity * 10 * Time.deltaTime;
        var y = Input.GetAxis("Mouse Y") * rotationSensitivity * 10 * Time.deltaTime;
        _xRotation -= y;
        _xRotation = Mathf.Clamp(_xRotation, -90, 90);
        playerCamera.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        transform.Rotate(Vector3.up * x);
    }

    private void MovePlayer()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        _playerController.Move((transform.right * x + transform.forward * y) * (movementSpeed * Time.deltaTime));
    }

    private void ManageInputs()
    {
        if (Input.GetKeyUp(KeyCode.F))
            flashlightObject.SetActive(!flashlightObject.activeSelf);
    }

}