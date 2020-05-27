using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerInterface))]
public class PlayerStats : MonoBehaviour
{

	private int _health;
	private bool _isDead;
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
		_isDead = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Finish"))
			_interface.ToggleWinnerScreen();
	}

	public void Damage(int amount)
	{
		_health -= amount;
		healthText.text = _health.ToString() + "%";
		if (_health <= 0 && !_isDead)
			_interface.ToggleDeathScreen();
	}

}