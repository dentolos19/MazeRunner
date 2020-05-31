using UnityEngine;

public class PlayerLook : MonoBehaviour
{

	private float _sensitivity;
	private float _xRotation;

	public Transform player;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		_sensitivity = Game.Settings.Sensitivity * 10;
	}
	
	private void Update()
	{
		var x = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
		var y = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;
		_xRotation -= y;
		_xRotation = Mathf.Clamp(_xRotation, -90, 90);
		transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
		player.Rotate(Vector3.up * x);
	}

}