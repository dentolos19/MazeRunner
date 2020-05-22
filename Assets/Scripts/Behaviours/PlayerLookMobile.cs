using UnityEngine;

public class PlayerLookMobile : MonoBehaviour
{

	private Camera _camera;
	private Vector3 _original;
	private float _sensitivity;
	private Vector3 _touchPos;

	private float _xRotation;
	private float _yRotation;
	
	public RectTransform area;

	private void Start()
	{
		if (!Game.IsMobilePlatform)
			return;
		_camera = Camera.main;
		if (_camera != null)
			_original = _camera.transform.eulerAngles;
		_xRotation = _original.x;
		_yRotation = _original.y;
		_sensitivity = Game.Settings.Sensitivity / 100;
	}

	private void FixedUpdate()
	{
		if (!Game.IsMobilePlatform)
			return;
		foreach (var touch in Input.touches)
		{
			var pos = _camera.ScreenToWorldPoint(touch.position);
			if (!area.rect.Contains(pos))
				return;
			switch (touch.phase)
			{
				case TouchPhase.Began:
					_touchPos = pos;
					break;
				case TouchPhase.Moved:
				{
					var x = _touchPos.x - pos.x;
					var y = _touchPos.y - pos.y;
					_xRotation -= y * _sensitivity * Time.deltaTime * -1;
					_yRotation += x * _sensitivity * Time.deltaTime * -1;
					_yRotation = Mathf.Clamp(_yRotation, -90, 90);
					_camera.transform.eulerAngles = new Vector3(_xRotation, _yRotation, 0);
					break;
				}
				case TouchPhase.Ended:
					_touchPos = new Vector2();
					break;
			}
		}
	}

}