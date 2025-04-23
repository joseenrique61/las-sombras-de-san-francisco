using UnityEngine;
using UnityEngine.InputSystem;

public class CandlePickup : MonoBehaviour
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
