using UnityEngine;
using UnityEngine.InputSystem;
using Player.UI;

namespace Player.Movement
{
    public class PlayerFootSteeps : MonoBehaviour
    {
        [Header("Audio Configuration")]
        [SerializeField] private float footSteepDelay;
        [SerializeField] private AudioClip[] audios;
        private float footSteepTimer;
        private Vector2 movement;
        private PlayerInput input;

        void Start()
        {
            input = GetComponent<PlayerInput>();
            footSteepTimer = 0f;
        }

        void Update()
        {
            movement = input.actions["Move"].ReadValue<Vector2>();

            if (movement.magnitude > 0.1f)
            {
                footSteepTimer -= Time.deltaTime;

                if (footSteepTimer <= 0f)
                {
                    Debug.Log("Step");
                    SoundsMannager.Instance.PlayRandomSFX(audios);
                    footSteepTimer = footSteepDelay;
                }
            }
            else if (footSteepTimer < footSteepDelay)
            {
                footSteepTimer = footSteepDelay;
            }
        }
    }
}
