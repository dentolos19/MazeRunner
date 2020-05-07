using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerLookTouch : MonoBehaviour
{

    private float _xRotation;
    private float _yRotation;
    private float _sensitivity;
    private Touch _touch;
    private Camera _camera;
    private Vector3 _original;

    private void Start()
    {
        if (!Game.IsMobilePlatform)
            return;
        _camera = Camera.main;
        if (_camera != null)
            _original = _camera.transform.eulerAngles;
        _xRotation = _original.x;
        _yRotation = _original.y;
        _sensitivity = Game.Settings.Sensitivity / 10;
    }
    
    private void FixedUpdate()
    {
        if (!Game.IsMobilePlatform)
            return;
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        foreach (var touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _touch = touch;
                    break;
                case TouchPhase.Moved:
                {
                    var x = _touch.position.x - touch.position.x;
                    var y = _touch.position.y - touch.position.y;
                    _xRotation -= y * _sensitivity * Time.deltaTime * -1;
                    _yRotation += x * _sensitivity * Time.deltaTime * -1;
                    _yRotation = Mathf.Clamp(_yRotation, -90, 90);
                    _camera.transform.eulerAngles = new Vector3(_xRotation, _yRotation, 0);
                    break;
                }
                case TouchPhase.Ended:
                    _touch = new Touch();
                    break;
            }
        }
    }

}