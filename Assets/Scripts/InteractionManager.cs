using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
	private enum InteractionType
	{
		Hide,
		Candle,
		None
	}

	private Hide hide;
	private CandlePickup candlePickup;

	private InteractionType currentInteractionPossibily = InteractionType.None;

	private void Start()
	{
		hide = GetComponent<Hide>();
		candlePickup = GetComponent<CandlePickup>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Closet"))
		{
			hide.SetCanHide(true, collision);
			currentInteractionPossibily = InteractionType.Hide;
		}
		else if (collision.CompareTag("CandlePickUp"))
		{
			candlePickup.SetCandleToPickup(collision.gameObject);
			currentInteractionPossibily = InteractionType.Candle;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Closet"))
		{
			hide.SetCanHide(false, collision);
		}
		else if (collision.CompareTag("CandlePickUp"))
		{
			candlePickup.SetCandleToPickup(null);
		}
		currentInteractionPossibily = InteractionType.None;
	}

	public void Interact(InputAction.CallbackContext callbackContext)
	{
		if (!callbackContext.started)
		{
			return;
		}

		switch (currentInteractionPossibily)
		{
			case InteractionType.Hide:
				hide.AlternateHidden();
				break;
			case InteractionType.Candle:
				candlePickup.PickupCandle();
				break;
			default:
				break;
		}
	}
}
