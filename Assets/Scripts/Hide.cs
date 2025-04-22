using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hide : MonoBehaviour
{
	private GameObject visuals;
	private GameObject candle;

	private BoxCollider2D closetCollider;
	private bool canHide = false;
	public bool IsHidden { get; private set; } = false;

	private Vector2 originalPosition;
	private PlayerInput input;

	public void Start()
	{
		input = GetComponent<PlayerInput>();
		visuals = transform.Find("PlayerVisuals").gameObject;
		candle = transform.Find("Candle").gameObject;
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

		if (canHide && !IsHidden)
		{
			visuals.SetActive(false);
			candle.SetActive(false);

			input.actions["Move"].Disable();
			closetCollider.enabled = false;
			originalPosition = transform.position;
			transform.position = closetCollider.transform.position;
			IsHidden = true;
		}
		else if (IsHidden)
		{
			visuals.SetActive(true);
			candle.SetActive(true);

			input.actions["Move"].Enable();
			transform.position = originalPosition;
			closetCollider.enabled = true;
			IsHidden = false;
		}
	}
}
