using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private float visionRange = 5f;

	public List<Vector2> Route;
	private int currentTarget = 0;

	private Hide playerHideComponent;

	private bool chasingPlayer = false;

	private NavMeshAgent agent;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
	}

	void Start()
	{
		playerHideComponent = player.GetComponent<Hide>();

		agent.SetDestination(Route[currentTarget]);
	}

	private void Update()
	{
		chasingPlayer = Vector2.Distance(transform.position, player.position) < visionRange;

		if (chasingPlayer && !playerHideComponent.IsHidden)
		{
			agent.SetDestination(player.position);
		}
		else if (chasingPlayer && playerHideComponent.IsHidden)
		{
			chasingPlayer = false;
			agent.SetDestination(Route[currentTarget]);
		}
		else if (Vector2.Distance(transform.position, Route[currentTarget]) < 0.2f)
		{
			currentTarget = GetNextPatrolPoint();
			agent.SetDestination(Route[currentTarget]);
		}
		else
		{
			agent.SetDestination(Route[currentTarget]);
		}
	}

	private int GetNextPatrolPoint()
	{
		return (currentTarget + 1) % Route.Count;
	}
}
