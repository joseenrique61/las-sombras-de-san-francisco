using UnityEngine;

namespace Player.UI
{
	public class CandleTimeUI : MonoBehaviour
	{
		[SerializeField] private float TopY;
		[SerializeField] private float BottomY;

		private PlayerController playerController;
		private RectTransform RectTransform;

		private void Start()
		{
			playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
			
			if (playerController == null) Debug.LogError("Hace falta el PlayerController en Player");

			RectTransform = GetComponent<RectTransform>();
		}

		void Update()
		{
			float t = Mathf.InverseLerp(0, playerController.lightDuration, playerController.remainingTimeLight);
			float output = Mathf.Lerp(BottomY, TopY, t);

			RectTransform.anchoredPosition = new Vector2(transform.position.x, output);
		}
	}
}