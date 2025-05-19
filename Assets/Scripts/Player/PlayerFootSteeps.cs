using UnityEngine;
using UnityEngine.InputSystem;
using Audio;
using System.Collections.Generic;

namespace Player
{
    public class PlayerFootSteeps : MonoBehaviour
    {
        [Header("FX Sounds")]
        [SerializeField] private float footSteepDelay;
        [SerializeField] private List<AudioClip> audios;
        private AudioController audioController;
        private float footSteepTimer;
        private Vector2 movement;
        private PlayerInput input;

        void Awake()
        {
            audioController = GetComponent<AudioController>();
            input = GetComponent<PlayerInput>();   
        }

        void Start()
        {
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
                    audioController.PlayRandomSFX(audios);
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
