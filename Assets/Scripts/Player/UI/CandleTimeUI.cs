using UnityEngine;
using Player.Items;

namespace Player.UI
{
	public class CandleTimeUI : MonoBehaviour
	{
		[SerializeField] private Candle candle;
		[SerializeField] private float TopY;
		[SerializeField] private float BottomY;

		private RectTransform RectTransform;

		private void Start()
		{
			RectTransform = GetComponent<RectTransform>();
		}

		void Update()
		{
			float t = Mathf.InverseLerp(0, candle.Duration, candle.TimeLeft);
			float output = Mathf.Lerp(BottomY, TopY, t);

			RectTransform.anchoredPosition = new Vector2(transform.position.x, output);
		}
	}
}