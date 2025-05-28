using UnityEngine;
using Interactions;
using Player;
using Ilumination;

namespace WorldElements
{
	public class Candle : MonoBehaviour, IInteractable
	{
		private GameObject candleToPickup;
		private InteractionObjectPrompt prompt;

		void Awake()
		{
			candleToPickup = gameObject;
			prompt = GetComponent<InteractionObjectPrompt>();
		}

		public void SetCandleToPickup(GameObject candleToPickup)
		{
			this.candleToPickup = candleToPickup;
		}

		public void PickupCandle(LightPoint lightPoint)
		{
			lightPoint.RestartCandleLight();
			Destroy(candleToPickup);
		}

		public void Interact(GameObject interactor)
		{
			PlayerController playerController = interactor.GetComponent<PlayerController>();
			PickupCandle(playerController.lightPoint);
		}

		public void ShowPrompt() => prompt?.ShowPrompt();
		public void HidePrompt() => prompt?.HidePrompt();


		public void OnEnterRange(GameObject interactor)
		{
			Debug.Log("Puede agarrar la llave");
		}

		public void OnExitRange(GameObject interactor)
		{
			Debug.Log("Ya no puede agarrar la llave");
		}
	}
	}