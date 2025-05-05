using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;
using Player.Actions;
using Player.UI;
using Player.Items;
using UnityEngine.SceneManagement;

namespace Enemy
{
	public class EnemyMovement : MonoBehaviour
	{
		[SerializeField] private GameObject player;
		[SerializeField] private GameObject enemy;
		[SerializeField] private float visionRange = 5f;
		[SerializeField, Range(0f, 1f)] private float marginToPlayer = 0.7f;
		private LevelLoader levelLoader;
		private Light2D playerCandle;
		private Hide playerHideComponent;
		private Transform playerTransform;
		private Animator enemyAnimator;
		private Vector2 movement;
		public List<Vector2> Route;
		private int currentTarget = 0;
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
			playerCandle = player.transform.Find("Candle").GetComponent<Light2D>();
			playerTransform = player.GetComponent<Transform>();
			playerHideComponent = player.GetComponent<Hide>();

			enemyAnimator = enemy.GetComponent<Animator>();
			levelLoader = GameObject.Find("ElementsUI").GetComponent<LevelLoader>();
	
			agent.SetDestination(Route[currentTarget]);
		}

		private void Update()
		{
			if (!TryUpdatePlayerReference()){
				Debug.Log("Error, el jugador ya no existe");
				return;
			} 
				
			chasingPlayer = Vector2.Distance(transform.position, playerTransform.position) < visionRange;

			if (chasingPlayer && !playerHideComponent.IsHidden)
			{
				Vector2 direction = playerTransform.position - transform.position;
				Vector2 dest = new Vector2(playerTransform.position.x, playerTransform.position.y) - marginToPlayer * playerCandle.pointLightOuterRadius * direction.normalized;
				
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

		private bool TryUpdatePlayerReference()
		{
			if (player == null || player.Equals(null))
			{
				GameObject playerObj = GameObject.FindWithTag("Player");
				if (playerObj != null)
				{
					playerTransform = playerObj.transform;
					playerHideComponent = playerObj.GetComponent<Hide>();
					playerCandle = player.transform.Find("Candle").GetComponent<Light2D>();
					return true;
				}
				return false;
			}
			return true;
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
}