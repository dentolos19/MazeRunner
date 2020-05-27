using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

	private float _damageCooldown;
	
	public int maxDamage = 5;
	public int minDamage = 2;
	public float damagePerSecond = 3;
	public float attackRadius = 1;

	private void Update()
	{
		var colliders = Physics.OverlapSphere(transform.position, attackRadius);
		foreach (var collider in colliders)
			if (collider.CompareTag("Player"))
			{
				if (!(_damageCooldown <= 0))
					continue;
				collider.GetComponent<PlayerStats>().Damage(Random.Range(2, 5));
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