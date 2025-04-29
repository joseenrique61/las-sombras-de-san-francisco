using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject candle;
	[SerializeField] private float speed;
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
	}

	void Update()
	{
		movement = input.actions["Move"].ReadValue<Vector2>();
		playerAnimator.SetFloat("movement",movement.magnitude * speed);

		if (movement.magnitude > 0.1f)
		{
			if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
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
			else
			{
				if (movement.y > 0)
					Debug.Log("Arriba");
				else
					Debug.Log("Abajo");
			}
		}
	}

	private void FixedUpdate()
	{
		rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement); 
	}
}
