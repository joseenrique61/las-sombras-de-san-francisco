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
				if (movement.x > 0)
				{
					playerSprite.flipX = false;
					candleSprite.flipX = true;
					candle.transform.position = new Vector2(-0.15f, 0.225f);
				}
				else
				{
					playerSprite.flipX = true;
					candleSprite.flipX = false;
					candle.transform.position = new Vector2(0.15f, 0.225f);
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
