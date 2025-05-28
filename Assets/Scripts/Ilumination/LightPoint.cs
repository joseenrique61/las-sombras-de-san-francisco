using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

namespace Ilumination
{
    public class LightPoint : MonoBehaviour
    {
        [Header("CandleLight Parameters")]
		[SerializeField] private float lightRadius = 6f;
		[SerializeField] private float noiseScale = 1.7f;
        public float lightDuration = 10f;
		public float remainingTimeLight { get; private set; }

        private Light2D light2D;

        void Start()
        {
            light2D = GetComponent<Light2D>();

            remainingTimeLight = lightDuration;
        }

        void Update()
        {
            UpdateLightStatus();
        }

        void UpdateLightStatus()
        {
            if (gameObject.activeSelf)
            {
                if (remainingTimeLight > 0)
                {
                    light2D.pointLightOuterRadius = Mathf.Lerp(0, lightRadius, remainingTimeLight / lightDuration);
                    remainingTimeLight -= Time.deltaTime;

                    float t = Mathf.InverseLerp(0, 1, Mathf.PerlinNoise1D(Time.time * noiseScale));
                    float intensity = Mathf.Lerp(1, 1.75f, t);

                    light2D.intensity = intensity;
                }
            }
        }

        public void RestartCandleLight()
		{
			remainingTimeLight = lightDuration;
			light2D.enabled = true;
		}


    }
}
