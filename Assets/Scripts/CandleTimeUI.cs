using UnityEngine;

public class CandleTimeUI : MonoBehaviour
{
	[SerializeField] private Candle Candle;
	[SerializeField] private float TopY;
	[SerializeField] private float BottomY;

	private RectTransform RectTransform;

	private void Start()
	{
		RectTransform = GetComponent<RectTransform>();
	}

	void Update()
	{
		float t = Mathf.InverseLerp(0, Candle.Duration, Candle.TimeLeft);
		float output = Mathf.Lerp(BottomY, TopY, t);

		RectTransform.anchoredPosition = new Vector2(transform.position.x, output);
	}
}
