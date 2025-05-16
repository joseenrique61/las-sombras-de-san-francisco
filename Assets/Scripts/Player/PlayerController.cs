using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;


namespace Player
{
	public class PlayerController : MonoBehaviour
	{
        [Header("Player")]
        [SerializeField] private GameObject player;
		[SerializeField] private GameObject candle;
		[SerializeField] private float speed;

        [Header("CandleLight Parameters")]
        public float lightDuration = 10f;
		[SerializeField] private float lightRadius = 6f;
		[SerializeField] private float noiseScale = 1.7f;
        public float remainingTimeLight { get; private set; }
        public bool isHidden { get; set; } = false;
        private Light2D light2D;
        private GameObject playerCandle;
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
			
            playerCandle = GameObject.FindWithTag("PlayerCandle")?.gameObject;
            light2D = playerCandle.GetComponent<Light2D>();

			speed = 4;
			rb = GetComponent<Rigidbody2D>();
			input = GetComponent<PlayerInput>();

			remainingTimeLight = lightDuration;
		}

		void Update()
		{
			HandleMovement();
            UpdateLightStatus();
		}

		void HandleMovement()
		{
			movement = input.actions["Move"].ReadValue<Vector2>();
			playerAnimator.SetFloat("movement",movement.magnitude * speed);

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

        void UpdateLightStatus()
        {
            if (playerCandle.activeSelf)
            {
                if (remainingTimeLight > 0)
                {
                    light2D.pointLightOuterRadius = Mathf.Lerp(0, lightRadius, remainingTimeLight / lightDuration);
                    remainingTimeLight -= Time.deltaTime;

                    float t = Mathf.InverseLerp(0, 1, Mathf.PerlinNoise1D(Time.time * noiseScale));
                    float intensity = Mathf.Lerp(1, 1.75f, t);

                    light2D.intensity = intensity;
                }
            }
        }

        public void RestartCandleLight()
		{
			remainingTimeLight = lightDuration;
			light2D.enabled = true;
		}

		public void SwitchLight(InputAction.CallbackContext callbackContext)
		{
			if (!callbackContext.started)
			{
				return;
			}

			playerCandle.SetActive(!playerCandle.gameObject.activeSelf);
		}

		private void FixedUpdate()
		{
			rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement); 
		}
	}
}