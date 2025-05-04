using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

namespace Player.UI
{
    public class OptionsButton : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;

        [SerializeField] private TMP_Dropdown resolutionDropdown;
        private Resolution[] resolutions;
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