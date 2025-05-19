using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Audio
{
    public class AudioController : MonoBehaviour
    {
        private AudioSource audioSource;
        private List<AudioClip> audios;
        void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            if (audioSource == null)
                Debug.LogError($"Hace falta un componente AudioSource en este gameobject: {gameObject.name}");
        }

        public void PlaySFX(AudioClip audio)
        {
            audioSource.PlayOneShot(audio);
        }
        
        public void PlayRandomSFX(List<AudioClip> clips)
        {
            audios = clips;
            if (audios.Any())
            {
                int index = Random.Range(0,audios.Count-1);
                audioSource.PlayOneShot(audios[index]);
            }
        }
    }
}