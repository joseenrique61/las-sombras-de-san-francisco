using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	public float speed;
	public Animator animator;
	private Vector2 movement;

	private Rigidbody2D rb;
	private PlayerInput input;

	private void Start()
	{	
		speed = 4;
		rb = GetComponent<Rigidbody2D>();
		input = GetComponent<PlayerInput>();
	}

	void Update()
	{
		movement = input.actions["Move"].ReadValue<Vector2>();
		animator.SetFloat("movement",movement.magnitude * speed);

		if (movement.magnitude > 0.1f)
		{
			if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
			{
				if (movement.x > 0)
					transform.localScale = new Vector3(1,1,1);
				else
					transform.localScale = new Vector3(-1,1,1);
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
