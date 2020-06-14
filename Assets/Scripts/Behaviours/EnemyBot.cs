using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBot : MonoBehaviour
{

	private NavMeshAgent _agent;
	
	public float playerDetectionRadius = 10;

	private void Start()
	{
		_agent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		var distance = Vector3.Distance(transform.position, EntityMonitor.Instance.player.position);
		if (distance <= playerDetectionRadius)
			_agent.SetDestination(EntityMonitor.Instance.player.position);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
	}
	
}