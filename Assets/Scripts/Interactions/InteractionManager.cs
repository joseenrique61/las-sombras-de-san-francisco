using UnityEngine;
using UnityEngine.InputSystem;
using Inventory;
using Player.Actions;

namespace Interactions
{
	public class InteractionManager : MonoBehaviour
	{
		private enum InteractionType
		{
			HideInCloset,
			PickUpCandleLight,
			PickUpItem,
			OpenDoor,
			None
		}

		private Hide currentCloset;
		private LightOnCandle currentCandleLight;
		private WorldItem currentWorldItem;
		private LockedDoor currentLockedDoor;
		private InteractionObjectPrompt interactionPrompt;
		private InteractionAnimation objectInteraction;
		private InteractionType currentInteractionPossibily = InteractionType.None;

		private void Start()
		{
			currentCloset = GetComponent<Hide>();
			currentCandleLight = GetComponent<LightOnCandle>();
			if (InventoryManager.Instance == null) {
				Debug.LogError("¡Falta InventoryManager en la escena o en el jugador!");
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			
			if (collision.CompareTag("Closet") || collision.CompareTag("CandlePickUp") || collision.CompareTag("LockedDoor") || collision.CompareTag("WorldItem"))
			{
				interactionPrompt = collision.GetComponent<InteractionObjectPrompt>();
				objectInteraction = collision.GetComponent<InteractionAnimation>();
			}

			if (collision.CompareTag("Closet"))
			{	
				currentCloset.SetCanHide(true, collision);
				currentInteractionPossibily = InteractionType.HideInCloset;
			}
			else if (collision.CompareTag("CandlePickUp"))
			{
				currentCandleLight.SetCandleToPickup(collision.gameObject);
				currentInteractionPossibily = InteractionType.PickUpCandleLight;
			}
			else if (collision.CompareTag("LockedDoor"))
			{
				currentLockedDoor = collision.GetComponent<LockedDoor>();
				if (currentLockedDoor != null && !currentLockedDoor.isOpen) 
				{
					currentInteractionPossibily = InteractionType.OpenDoor;
				} 
				else if (currentLockedDoor != null && currentLockedDoor.isOpen) 
				{
					interactionPrompt = null;
					currentInteractionPossibily = InteractionType.None;
				} 
			}
			else if (collision.CompareTag("WorldItem"))
			{
				currentWorldItem = collision.GetComponent<WorldItem>();
				if (currentWorldItem != null)
					currentInteractionPossibily = InteractionType.PickUpItem;
			}
			else currentInteractionPossibily = InteractionType.None;
			

			if (currentInteractionPossibily != InteractionType.None && interactionPrompt != null)
				interactionPrompt.ShowPrompt();
			else
			{
				interactionPrompt.HidePrompt();
				interactionPrompt = null;
			}
		}

		private void OnTriggerExit2D(Collider2D collision)
		{

			bool exitingCurrentInteractable = false;
			switch(currentInteractionPossibily)
			{
				case InteractionType.HideInCloset: exitingCurrentInteractable = collision.CompareTag("Closet"); break;
				case InteractionType.PickUpCandleLight: exitingCurrentInteractable = collision.CompareTag("CandlePickUp"); break;
				case InteractionType.PickUpItem: exitingCurrentInteractable = collision.CompareTag("WorldItem") && collision.gameObject == currentWorldItem?.gameObject; break;
				case InteractionType.OpenDoor: exitingCurrentInteractable = collision.CompareTag("LockedDoor") && collision.gameObject == currentLockedDoor?.gameObject; break;
			}

			if (exitingCurrentInteractable)
				{
					if (collision.CompareTag("Closet"))
				{
					currentCloset.SetCanHide(false, collision);
				}
				else if (collision.CompareTag("CandlePickUp"))
				{
					currentCandleLight.SetCandleToPickup(null);
				}

				currentInteractionPossibily = InteractionType.None;

				interactionPrompt.HidePrompt();
				objectInteraction?.ResetValues();
				
				currentWorldItem = null;
				currentLockedDoor = null;

				interactionPrompt = null;
				objectInteraction = null;
			}
		}

		public void Interact(InputAction.CallbackContext callbackContext)
		{
			if (!callbackContext.started)
			{
				return;
			}

			if (!Equals(objectInteraction, null) && !Equals(currentInteractionPossibily,InteractionType.None))
			{
				objectInteraction.PlayOnceAnimation();
				switch (currentInteractionPossibily)
				{
					case InteractionType.HideInCloset:
						currentCloset.AlternateHidden();
						break;
					case InteractionType.PickUpCandleLight:
						currentCandleLight.PickupCandle();
						break;
					case InteractionType.PickUpItem:
						if (currentWorldItem != null)
						{
							if (InventoryManager.Instance.AddItem(currentWorldItem.itemData))
							{
								Debug.Log($"Recogido {currentWorldItem.itemData.displayName} mediante interacción.");
								currentWorldItem.PickUpItem();
								currentInteractionPossibily = InteractionType.None;
								currentWorldItem = null;
							} 
						}
						break;
					case InteractionType.OpenDoor:
						currentLockedDoor?.AttemptOpen();
						if (currentLockedDoor.isOpen) 
						{
							Debug.Log("Puerta abierta");
							currentInteractionPossibily = InteractionType.None;
						}
							
						//interactionPrompt?.HidePrompt();
						//	interactionPrompt = null;
						//	currentLockedDoor = null; // Ojo: si quieres poder cerrarla, no limpies esto.
						break;
					default:
						break;
				}
			}
		}
	}
}
