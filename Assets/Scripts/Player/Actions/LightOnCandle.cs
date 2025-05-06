using UnityEngine;
using UnityEngine.InputSystem;
using Player.Items;

namespace Player.Actions
{
	public class LightOnCandle : MonoBehaviour
	{
		[SerializeField] private Candle candle;
		private GameObject candleToPickup;
		public void SwitchLight(InputAction.CallbackContext callbackContext)
		{
			if (!callbackContext.started)
			{
				return;
			}

			candle.gameObject.SetActive(!candle.gameObject.activeSelf);
		}

		public void SetCandleToPickup(GameObject candleToPickup)
		{
			this.candleToPickup = candleToPickup;
		}

		public void PickupCandle()
		{
			candle.RestartCandleLight();
			Destroy(candleToPickup);
		}
	}
}