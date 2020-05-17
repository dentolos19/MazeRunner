using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

	private float _timer;
	
	public int damage = 1;
	public int damagePerSecond = 5;
	public float range = 1;
	public LayerMask layer;
	
	private void Update()
	{
		Attack();
	}

	private void Attack()
	{
		var enemies = Physics.OverlapSphere(transform.position, range, layer);
		foreach (var enemy in enemies)
		{
			if (!(_timer <= 0))
				continue;
			enemy.GetComponent<PlayerStats>().RecieveDamage(damage);
			_timer = damagePerSecond;
		}
		_timer -= Time.deltaTime;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, range);
	}

}