using Ilumination;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
	public class PlayerController : MonoBehaviour
	{
		[Header("Player")]
		[SerializeField] private GameObject player;
		[SerializeField] private GameObject candle;
		[SerializeField] private float speed;
		public bool isHidden { get; set; } = false;
        private GameObject playerCandle;
		public LightPoint lightPoint { get; private set; }
		private Animator playerAnimator;
		private Vector2 movement;
		private SpriteRenderer candleSprite;
		private SpriteRenderer playerSprite;
		private Rigidbody2D rb;
		private PlayerInput input;

		private void Start()
		{
			playerSprite = player.GetComponent<SpriteRenderer>();
			candleSprite = candle.GetComponent<SpriteRenderer>();
			playerAnimator = player.GetComponent<Animator>();

			speed = 4;
			rb = GetComponent<Rigidbody2D>();
			input = GetComponent<PlayerInput>();

			playerCandle = GameObject.FindWithTag("Light").gameObject;
			lightPoint = GameObject.FindWithTag("Light").GetComponent<LightPoint>();
		}

		void Update()
		{
			HandleMovement();
		}

		void HandleMovement()
		{
			movement = input.actions["Move"].ReadValue<Vector2>();
			playerAnimator.SetFloat("movement", movement.magnitude * speed);

			if (movement.magnitude > 0.1f)
			{
				// Right movement
				if (movement.x > 0)
				{
					playerSprite.flipX = false;
					candleSprite.flipX = true;
					candleSprite.transform.localPosition = new Vector3(-0.15f, 0.225f, 0f);
				}

				// Left movement
				else
				{
					playerSprite.flipX = true;
					candleSprite.flipX = false;
					candleSprite.transform.localPosition = new Vector3(0.15f, 0.225f, 0f);
				}
			}
		}

		private void FixedUpdate()
		{
			rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement);
		}
		
		public void SwitchLight(InputAction.CallbackContext callbackContext)
		{
			if (!callbackContext.started)
				return;

			playerCandle.SetActive(!playerCandle.gameObject.activeSelf);
		}
	}
}