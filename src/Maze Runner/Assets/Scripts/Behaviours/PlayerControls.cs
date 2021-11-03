using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    private float _xRotation;
    private GameMaster _gameMaster;

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
        _gameMaster = FindObjectOfType<GameMaster>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        RotateCamera();
        MovePlayer();
        ManageInputs();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish")) // ends game when goal is reached
            _gameMaster.EndGame(true); // via game master
    }

    private void RotateCamera()
    {
        var x = Input.GetAxis("Mouse X") * rotationSensitivity * 10 * Time.deltaTime;
        var y = Input.GetAxis("Mouse Y") * rotationSensitivity * 10 * Time.deltaTime;
        _xRotation -= y;
        _xRotation = Mathf.Clamp(_xRotation, -90, 90); // clamps vertical rotation
        playerCamera.localRotation = Quaternion.Euler(_xRotation, 0, 0); // rotates vertically
        transform.Rotate(Vector3.up * x); // rotates horizontally
    }

    private void MovePlayer()
    {
        var x = Input.GetAxis("Horizontal"); // gets left/right movement
        var y = Input.GetAxis("Vertical"); // gets forward/backward movement
        _playerController.Move((transform.right * x + transform.forward * y) * (movementSpeed * Time.deltaTime));
    }

    private void ManageInputs()
    {
        if (Input.GetKeyUp(KeyCode.F)) // toggles flashlight
            flashlightObject.SetActive(!flashlightObject.activeSelf);
        if (Input.GetKeyUp(KeyCode.Escape)) // toggles pause menu
            _gameMaster.TogglePauseMenu(); // via game master
    }

}