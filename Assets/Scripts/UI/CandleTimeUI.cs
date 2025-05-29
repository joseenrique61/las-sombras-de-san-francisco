using Ilumination;
using System;
using UnityEngine;

namespace UI
{
	public class CandleTimeUI : MonoBehaviour
	{
		[SerializeField] private float TopY;
		[SerializeField] private float BottomY;

		private LightPoint lightPoint;
		private RectTransform RectTransform;

		private void Start()
		{
			lightPoint = GameObject.FindWithTag("Light").GetComponent<LightPoint>();
			
			if (lightPoint == null) Debug.LogError("Hace falta el PlayerController en Player");

			RectTransform = GetComponent<RectTransform>();
		}

		void Update()
		{
			float t = Mathf.InverseLerp(0, lightPoint.lightDuration, lightPoint.remainingTimeLight);
			float output = Mathf.Lerp(BottomY, TopY, t);

			RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, output);
		}
	}
}