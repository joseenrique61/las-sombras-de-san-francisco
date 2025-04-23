using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsMannager : MonoBehaviour
{
    public static SoundsMannager Instance;
    public AudioSource audioSource;
    private AudioClip[] audios; 
    // Start is called before the first frame update
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
