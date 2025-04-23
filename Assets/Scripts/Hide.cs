using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hide : MonoBehaviour
{
	private GameObject visuals;
	private GameObject candle;

	private BoxCollider2D closetCollider;
	public bool IsHidden { get; private set; } = false;

	private Vector2 originalPosition;
	private PlayerInput input;

	public void Start()
	{
		input = GetComponent<PlayerInput>();
		visuals = transform.Find("PlayerVisuals").gameObject;
		candle = transform.Find("Candle").gameObject;
	}

	public void SetCanHide(bool canHide, Collider2D collision)
	{
		if (canHide)
		{
			closetCollider = collision.GetComponents<BoxCollider2D>().FirstOrDefault(x => x.isTrigger == false);
		}
		else
		{
			closetCollider = null;
		}
	}

	public void AlternateHidden()
	{
		if (!IsHidden)
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
