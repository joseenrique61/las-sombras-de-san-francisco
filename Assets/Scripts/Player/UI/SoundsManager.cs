using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.UI
{
    public class SoundsMannager : MonoBehaviour
    {
        public static SoundsMannager Instance;
        public AudioSource audioSource;
        private AudioClip[] audios; 
        void Awake()
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySFX(AudioClip audio)
        {
            audioSource.PlayOneShot(audio);
        }

        public void PlayRandomSFX(AudioClip[] clips)
        {
            audios = clips;
            if (audios.Length > 0)
            {
                int index = Random.Range(0,audios.Length);
                audioSource.PlayOneShot(audios[index]);
            }
        }
    }
}