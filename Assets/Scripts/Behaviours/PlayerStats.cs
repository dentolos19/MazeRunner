using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerInterface))]
public class PlayerStats : MonoBehaviour
{

	private int _health;
	private PlayerInterface _interface;

	public int maxHealth = 100;
	public TextMeshProUGUI healthText;

	private void Awake()
	{
		_interface = GetComponent<PlayerInterface>();
	}

	private void Start()
	{
		_health = maxHealth;
	}

	private void Update()
	{
		healthText.text = _health.ToString();
		if (_health <= 0)
			_interface.ToggleDeathScreen();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Finish"))
			_interface.ToggleWinnerScreen();
	}

	public void Damage(int amount)
	{
		_health -= amount;
	}

}