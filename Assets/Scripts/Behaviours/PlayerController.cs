using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    private int _leftFingerId;
    private int _rightFingerId;
    private float _cameraPitch;
    private float _sensitivity;
    private float _halfScreenWidth;
    private float _inputDeadzone;
    private CharacterController _controller;
    private Vector2 _lookInput;
    private Vector2 _moveInput;
    private Vector2 _moveInputStart;

    public float speed = 5;
    public Transform cameraObject;
    
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _halfScreenWidth = Screen.width / 2;
        _inputDeadzone = Mathf.Pow(Screen.height / 10, 2);
    }
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _sensitivity = Game.RunningOnMobile ? Game.Settings.Sensitivity / 2 : Game.Settings.Sensitivity * 10;
        _leftFingerId = -1;
        _rightFingerId = -1;
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
                        if (touch.position.x < _halfScreenWidth && _leftFingerId <= -1)
                        {
                            _leftFingerId = touch.fingerId;
                            _moveInputStart = touch.position;
                        }
                        else if (touch.position.x > _halfScreenWidth && _rightFingerId <= -1)
                        {
                            _rightFingerId = touch.fingerId;
                        }
                        break;
                    case TouchPhase.Stationary:
                        if (touch.fingerId == _rightFingerId)
                            _lookInput = Vector2.zero;
                        break;
                    case TouchPhase.Moved:
                        if (touch.fingerId == _leftFingerId)
                            _moveInput = touch.position - _moveInputStart;
                        else if (touch.fingerId == _rightFingerId)
                            _lookInput = touch.deltaPosition * (_sensitivity * Time.deltaTime);
                        break;
                    case TouchPhase.Canceled:
                    case TouchPhase.Ended:
                        if (touch.fingerId == _leftFingerId)
                            _leftFingerId = -1;
                        else if (touch.fingerId == _rightFingerId)
                            _rightFingerId = -1;
                        break;
                }
            }
            if (!(_leftFingerId <= -1))
                TouchMove();
            if (!(_rightFingerId <= -1))
                TouchLook();
        }
        else
        {
            MouseLook();
            KeyboardMove();   
        }
    }

    private void MouseLook()
    {
        var mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;
        _cameraPitch -= mouseY;
        _cameraPitch = Mathf.Clamp(_cameraPitch, -90, 90);
        cameraObject.localRotation = Quaternion.Euler(_cameraPitch, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void KeyboardMove()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var motion = transform.right * horizontal + transform.forward * vertical;
        _controller.Move(motion * (speed * Time.deltaTime));
    }

    private void TouchLook()
    {
        _cameraPitch = Mathf.Clamp(_cameraPitch - _lookInput.y, -90, 90);
        cameraObject.localRotation = Quaternion.Euler(_cameraPitch, 0, 0);
        transform.Rotate(transform.up, _lookInput.x);
    }

    private void TouchMove()
    {
        if (_moveInput.sqrMagnitude <= _inputDeadzone)
            return;
        var direction = _moveInput.normalized * (speed * Time.deltaTime);
        var motion = transform.right * direction.x + transform.forward * direction.y;
        _controller.Move(motion);
    }

}