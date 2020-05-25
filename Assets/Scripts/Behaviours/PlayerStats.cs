using UnityEngine;

[RequireComponent(typeof(PlayerInterface))]
public class PlayerStats : MonoBehaviour
{

	private PlayerInterface _interface;
	
	public int health = 100;

	private void Awake()
	{
		_interface = GetComponent<PlayerInterface>();
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Finish"))
			_interface.ToggleWinnerScreen();
	}

}