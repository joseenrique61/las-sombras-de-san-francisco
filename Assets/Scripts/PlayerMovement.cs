using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	public float speed;

	private Vector2 movement;

	private Rigidbody2D rb;
	private PlayerInput input;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		input = GetComponent<PlayerInput>();
	}

	void Update()
	{
		movement = input.actions["Move"].ReadValue<Vector2>();
	}

	private void FixedUpdate()
	{
		rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement); 
	}
}
