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
	private InteractableObject interactionPrompt;
	private InteractionType currentInteractionPossibily = InteractionType.None;

	private void Start()
	{
		hide = GetComponent<Hide>();
		candlePickup = GetComponent<CandlePickup>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Closet") || collision.CompareTag("CandlePickUp"))
		{
			interactionPrompt = collision.GetComponent<InteractableObject>();
		}

		if (collision.CompareTag("Closet"))
		{	
			Debug.Log("Se puede esconder");
			hide.SetCanHide(true, collision);
			// hide.ShowPrompt(interactionPrompt);
			currentInteractionPossibily = InteractionType.Hide;
		}
		else if (collision.CompareTag("CandlePickUp"))
		{
			candlePickup.SetCandleToPickup(collision.gameObject);
			// candlePickup.ShowPrompt(interactionPrompt);
			currentInteractionPossibily = InteractionType.Candle;
		}
		interactionPrompt.ShowPrompt();
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Closet"))
		{
			hide.SetCanHide(false, collision);
			// hide.HidePrompt();
		}
		else if (collision.CompareTag("CandlePickUp"))
		{
			candlePickup.SetCandleToPickup(null);
			// candlePickup.HidePrompt();
		}
		currentInteractionPossibily = InteractionType.None;
		interactionPrompt.HidePrompt();
		interactionPrompt = null;
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
