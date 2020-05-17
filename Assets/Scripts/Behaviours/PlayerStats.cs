using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerInterface))]
public class PlayerStats : MonoBehaviour
{

	private int _health;
	private PlayerInterface _interface;
	
	public int maxHealth = 100;
	public TextMeshProUGUI healthText;

	private void Start()
	{
		_health = maxHealth;
		_interface = GetComponent<PlayerInterface>();
	}

	private void Update()
	{
		if (_health >= 1)
			return;
		_interface.ShowDeathScreen();
	}

	public void RecieveDamage(int damage)
	{
		if (_health <= 0)
			return;
		_health -= damage;
		healthText.text = $"{_health}%";
	}

}
