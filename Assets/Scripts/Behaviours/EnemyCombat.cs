using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

	private float _damageCooldown;
	
	public int damage = 5;
	public float damagePerSecond = 3;
	public float attackRadius = 1;

	private void Update()
	{
		var colliders = Physics.OverlapSphere(transform.position, attackRadius);
		foreach (var colldier in colliders)
			if (colldier.CompareTag("Player"))
			{
				if (!(_damageCooldown <= 0))
					continue;
				colldier.GetComponent<PlayerStats>().Damage(damage);
				_damageCooldown = damagePerSecond;
			}
		_damageCooldown -= Time.deltaTime;
	}
	
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRadius);
	}

}