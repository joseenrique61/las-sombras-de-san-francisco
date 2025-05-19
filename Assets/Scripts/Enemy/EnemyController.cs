using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;
using Player.UI;
using Audio;
using Player;

namespace Enemy
{
	public class EnemyController : MonoBehaviour
	{
		[Header("Settings")]
		[SerializeField] private float visionRange = 5f;
		[SerializeField, Range(0f, 1f)] private float marginToPlayer = 0.7f;
		[SerializeField] private List<Vector2> Route;

		[Header("Sounds FX")]
		[SerializeField] private AudioClip killingPlayerSound;
		[SerializeField] private List<AudioClip> aditionalEnemySounds;
		private AudioController audioController;
		private GameObject player;
		private LevelLoader levelLoader;
		private Light2D playerCandle;
		private PlayerController playerController;
		private Transform playerTransform;
		private Animator enemyAnimator;
		private int currentTarget = 0;
		private bool chasingPlayer = false;
		private NavMeshAgent agent;
		private void Awake()
		{
			audioController = GetComponent<AudioController>();

			agent = GetComponent<NavMeshAgent>();
			agent.updateRotation = false;
			agent.updateUpAxis = false;

			enemyAnimator = transform.Find("EnemyVisual").GetComponent<Animator>();
		}
		void Start()
		{
			player = GameObject.FindWithTag("Player");
			playerCandle = GameObject.FindWithTag("PlayerCandle").GetComponent<Light2D>();
			levelLoader = GameObject.Find("ElementsUI").GetComponent<LevelLoader>();

			playerTransform = player.GetComponent<Transform>();
			playerController = player.GetComponent<PlayerController>();

			agent.SetDestination(Route[currentTarget]);
		}

		private void Update()
		{
			chasingPlayer = Vector2.Distance(transform.position, playerTransform.position) < visionRange;
			
			if (chasingPlayer && !playerController.isHidden)
			{
				Vector2 direction = playerTransform.position - transform.position;
				Vector2 dest = new Vector2(playerTransform.position.x, playerTransform.position.y) - marginToPlayer * (playerCandle.gameObject.activeSelf ? playerCandle.pointLightOuterRadius : 0.01f) * direction.normalized;

				Debug.Log("Outer Radius: "+ playerCandle.pointLightOuterRadius + visionRange);
				Debug.Log("Direction sqrMagnitude:"+ direction.sqrMagnitude);
			
				if (direction.sqrMagnitude - visionRange == playerCandle.pointLightOuterRadius)
					audioController.PlayRandomSFX(aditionalEnemySounds);

				agent.SetDestination(dest);			
			}
			else if (chasingPlayer && playerController.isHidden)
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

			if (enemyAnimator != null)
            {
				Vector2 movement = (agent.transform.position - agent.destination).normalized;
				enemyAnimator.SetFloat("MovementX",movement.x);
    	        enemyAnimator.SetFloat("MovementY",movement.y);

				if (movement.x != 0 || movement.y !=0)
				{
					enemyAnimator.SetFloat("LastPositionX",movement.x);
    	            enemyAnimator.SetFloat("LastPositionY",movement.y);
				}
            }
		}

		private int GetNextPatrolPoint()
		{
			return (currentTarget + 1) % Route.Count;
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				audioController.PlaySFX(killingPlayerSound);
				levelLoader.RestartLevel();
			}
		}
	}
}