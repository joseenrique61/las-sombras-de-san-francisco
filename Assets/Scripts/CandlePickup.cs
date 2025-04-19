using UnityEngine;

public class CandlePickup : MonoBehaviour
{
	[SerializeField] private Candle candle;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("CandlePickUp"))
		{
			candle.RestartCandleLight();
			Destroy(collision.gameObject);
		}
	}
}
