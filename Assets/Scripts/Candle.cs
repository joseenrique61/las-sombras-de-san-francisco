using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Candle : MonoBehaviour
{
	[SerializeField] private float duration = 10f;
	[SerializeField] private float lightRadius = 6f;

	private float timer;
	private Light2D candleLight;

	void Start()
	{
		candleLight = GetComponent<Light2D>();
		timer = duration;
	}

	void Update()
	{
		timer -= Time.deltaTime;
		candleLight.pointLightOuterRadius = Mathf.Lerp(0, lightRadius, timer / duration);
		if (timer <= 0)
		{
			candleLight.enabled = false;
		}
	}

	public void RestartCandleLight()
	{
		timer = duration;
		candleLight.enabled = true;
	}
}
