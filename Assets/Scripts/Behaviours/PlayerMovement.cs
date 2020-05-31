using UnityEngine;

<<<<<<< HEAD
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

	private CharacterController _controller;

	public float speed = 5;

	private void Awake()
	{
		_controller = GetComponent<CharacterController>();
	}
	
	private void Update()
	{
		var x = Input.GetAxis("Horizontal");
		var z = Input.GetAxis("Vertical");
		var body = transform;
		var move = body.right * x + body.forward * z;
		_controller.Move(move * (speed * Time.deltaTime));
	}
	
=======
public class PlayerMovement : MonoBehaviour
{

    private CharacterController _controller;

    public float speed = 5;
    public GameObject mobileControls;
    public Joystick mobileJoystick;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        mobileControls.SetActive(Game.IsMobilePlatform);
    }

    private void Update()
    {
        float x;
        float z;
        if (Game.IsMobilePlatform)
        {
            x = mobileJoystick.Horizontal;
            z = mobileJoystick.Vertical;
        }
        else
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
        }
        var thing = transform;
        var move = thing.right * x + thing.forward * z;
        _controller.Move(move * (speed * Time.deltaTime));
    }

>>>>>>> RunAwayHaroldOld/master-old
}