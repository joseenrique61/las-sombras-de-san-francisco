using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("UI Settings")]
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        private Resolution[] resolutions;

        public void ContinueGameplay()
        {
            Time.timeScale = 1f;
        }
        
        void Start()
        {
            resolutions = Screen.resolutions;
            List<string> options = new List<string>();

            int currentResolutionIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.ClearOptions();
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
        }
        public void ChangeVolume(float volume)
        {
            audioMixer.SetFloat("VolumeParameter",volume);
        }

        public void EnterFullScreen()
        {
            Screen.fullScreen = true;
        }
    }
}