using UnityEngine;

public class PlayerLookMobile : MobileTouchEvents
{

    private float _pitch;
    private float _yaw;
    private float _sensitivity;
    private Vector3 _rotation;

    public Transform player;

    private void Start()
    {
        _sensitivity = Game.Settings.Sensitivity * 10;
        _rotation = player.eulerAngles;
        _pitch = _rotation.x;
        _yaw = _rotation.y;
    }

    protected override void OnTouchBegan()
    {
        currentTouchWatch = CurrentTouchId;
    }

    protected override void OnTouchMoved()
    {
        _pitch -= Input.GetTouch(currentTouchWatch).deltaPosition.y * _sensitivity * Time.deltaTime;
        _yaw += Input.GetTouch(currentTouchWatch).deltaPosition.x * _sensitivity * Time.deltaTime;
        _pitch = Mathf.Clamp(_pitch, -80, 80);
        player.eulerAngles = new Vector3 ( _pitch, _yaw, 0.0f);
    }

    protected override void OnTouchEnded()
    {
        if(CurrentTouchId == currentTouchWatch || Input.touches.Length <= 0)
            currentTouchWatch = 64;
    }

}