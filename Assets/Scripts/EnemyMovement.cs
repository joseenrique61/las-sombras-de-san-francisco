using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private Light2D candle;
	[SerializeField] private GameObject enemy;
	[SerializeField] private float visionRange = 5f;
	[SerializeField, Range(0f, 1f)] private float marginToPlayer = 0.7f;
	private Animator enemyAnimator;
	private SpriteRenderer enemySprite;
	private Vector2 movement;
	private LevelLoader levelLoader;
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
		enemySprite = enemy.GetComponent<SpriteRenderer>();
		enemyAnimator = enemy.GetComponent<Animator>();
		playerHideComponent = player.GetComponent<Hide>();

		levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();

		agent.SetDestination(Route[currentTarget]);
	}

	private void Update()
	{
		chasingPlayer = Vector2.Distance(transform.position, player.position) < visionRange;

		if (chasingPlayer && !playerHideComponent.IsHidden)
		{
			Vector2 direction = player.position - transform.position;
			Vector2 dest = new Vector2(player.position.x, player.position.y) - marginToPlayer * candle.pointLightOuterRadius * direction.normalized;
			
			agent.SetDestination(dest);			
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

		movement = (agent.transform.position - agent.destination).normalized;

		enemyAnimator.SetFloat("MovementX",movement.x);
		enemyAnimator.SetFloat("MovementY",movement.y);		
	}

	private int GetNextPatrolPoint()
	{
		return (currentTarget + 1) % Route.Count;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			levelLoader.RestartLevel();
		}
	}
}
