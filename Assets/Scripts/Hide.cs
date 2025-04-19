using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hide : MonoBehaviour
{
	private BoxCollider2D closetCollider;
	private bool canHide = false;
	private bool isHidden = false;

	private Vector2 originalPosition;
	private PlayerInput input;

	public void Start()
	{
		input = GetComponent<PlayerInput>();
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Closet"))
		{
			canHide = true;
			closetCollider = collision.GetComponents<BoxCollider2D>().FirstOrDefault(x => x.isTrigger == false);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Closet"))
		{
			canHide = false;
			closetCollider = null;
		}
	}

	public void AlternateHidden(InputAction.CallbackContext context)
	{
		if (!context.performed)
		{
			return;
		}

		if (canHide && !isHidden)
		{
			input.actions["Move"].Disable();
			closetCollider.enabled = false;
			originalPosition = transform.position;
			transform.position = closetCollider.transform.position;
			isHidden = true;
		}
		else if (isHidden)
		{
			input.actions["Move"].Enable();
			transform.position = originalPosition;
			closetCollider.enabled = true;
			isHidden = false;
		}
	}
}
