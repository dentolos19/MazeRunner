using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    private CharacterController _controller;

    public Joystick joystick;
    public float speed = 5;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if (Game.RunningOnMobile)
        {
            horizontal = joystick.Horizontal;
            vertical = joystick.Vertical;
        }
        var motion = transform.right * horizontal + transform.forward * vertical;
        if (_controller != null)
            _controller.Move(motion * (speed * Time.deltaTime));
    }
    
}