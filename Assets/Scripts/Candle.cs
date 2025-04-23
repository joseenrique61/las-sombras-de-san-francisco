using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Candle : MonoBehaviour
{
	public float Duration = 10f;
	[SerializeField] private float lightRadius = 6f;

	public float TimeLeft { get; private set; }

	private Light2D candleLight;

	void Start()
	{
		candleLight = GetComponent<Light2D>();
		TimeLeft = Duration;
	}

	void Update()
	{
		if (TimeLeft > 0)
		{
			candleLight.pointLightOuterRadius = Mathf.Lerp(0, lightRadius, TimeLeft / Duration);
			TimeLeft -= Time.deltaTime;
		}
	}

	public void RestartCandleLight()
	{
		TimeLeft = Duration;
		candleLight.enabled = true;
	}
}
