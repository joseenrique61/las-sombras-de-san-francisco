using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Player.Items
{
	public class Candle : MonoBehaviour
	{
		public float Duration = 10f;
		[SerializeField] private float lightRadius = 6f;
		[SerializeField] private float noiseScale = 1.7f;

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

				float t = Mathf.InverseLerp(0, 1, Mathf.PerlinNoise1D(Time.time * noiseScale));
				float intensity = Mathf.Lerp(1, 1.75f, t);

				candleLight.intensity = intensity;
			}
		}

		public void RestartCandleLight()
		{
			TimeLeft = Duration;
			candleLight.enabled = true;
		}
	}
}