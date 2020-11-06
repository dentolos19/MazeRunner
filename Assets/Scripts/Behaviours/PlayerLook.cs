using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    private int _fingerId;
    private float _cameraPitch;
    private float _sensitivity;
    private float _halfScreenWidth;
    
    private Vector2 _lookInput;

    public Transform playerObject;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _fingerId = -1;
        _sensitivity = Game.RunningOnMobile ? Game.Settings.Sensitivity / 2 : Game.Settings.Sensitivity * 10;
        _halfScreenWidth = Screen.width / 2;
    }

    private void Update()
    {
        if (Game.RunningOnMobile)
        {
            foreach (var touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (touch.position.x > _halfScreenWidth && _fingerId <= -1)
                            _fingerId = touch.fingerId;
                        break;
                    case TouchPhase.Canceled:
                    case TouchPhase.Ended:
                        if (_fingerId == touch.fingerId)
                            _fingerId = -1;
                        break;
                    case TouchPhase.Moved:
                        if (_fingerId == touch.fingerId)
                            _lookInput = touch.deltaPosition * (_sensitivity * Time.deltaTime);
                        break;
                    case TouchPhase.Stationary:
                        if (_fingerId == touch.fingerId)
                            _lookInput = Vector2.zero;
                        break;
                }
            }
            if (!(_fingerId <= -1))
                return;
            _cameraPitch -= _lookInput.y;
            _cameraPitch = Mathf.Clamp(_cameraPitch, -90, 90);
            transform.localRotation = Quaternion.Euler(_cameraPitch, 0, 0);
            playerObject.Rotate(transform.up * _lookInput.x);
        }
        else
        {
            var mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;
            _cameraPitch -= mouseY;
            _cameraPitch = Mathf.Clamp(_cameraPitch, -90, 90);
            transform.localRotation = Quaternion.Euler(_cameraPitch, 0, 0);
            playerObject.Rotate(Vector3.up * mouseX);
        }
    }
    
}