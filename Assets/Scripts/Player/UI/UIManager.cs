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
        //[SerializeField] private GameObject GameMenuPanel;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        private Resolution[] resolutions;
        /*
        private bool PausedGameplay;
        public void PauseGameplay()
        {
            PausedGameplay = true;
            Time.timeScale = 0f;
        }

        

        public void ToggleMenuPanel(InputAction.CallbackContext callbackContext)
        {
            if (!callbackContext.started) return;

            if(GameMenuPanel != null && PausedGameplay==false)
            {
                Debug.Log("The game has been paused");
                PauseGameplay();
                GameMenuPanel.SetActive(!GameMenuPanel.activeSelf);
                return;
            }
            
            if(GameMenuPanel != null && PausedGameplay==true)
            {
                Debug.Log("The game has been reanuded");
                ContinueGameplay();
                GameMenuPanel.SetActive(!GameMenuPanel.activeSelf);
                return;
            }
        }

        public void ToggleInventoryPanel(InputAction.CallbackContext callbackContext)
        {
            if (!callbackContext.started) return;

            if(InventoryPanel != null)
            {
                InventoryPanel.SetActive(!InventoryPanel.activeSelf);
            }
        }

        public void ToggleMenuPanel()
        {
            if(GameMenuPanel != null && PausedGameplay==false)
            {
                Debug.Log("The game has been paused");
                PauseGameplay();
                GameMenuPanel.SetActive(!GameMenuPanel.activeSelf);
                return;
            }
            
            if(GameMenuPanel != null && PausedGameplay==true)
            {
                Debug.Log("The game has been reanuded");
                ContinueGameplay();
                GameMenuPanel.SetActive(!GameMenuPanel.activeSelf);
                return;
            }
        }

        public void ToggleInventoryPanel()
        {
            if(InventoryPanel != null)
            {
                InventoryPanel.SetActive(!InventoryPanel.activeSelf);
            }
        }*/
        public void ContinueGameplay()
        {
            Time.timeScale = 1f;
        }
        
        void Start()
        {
            /*if(GameMenuPanel != null) 
                GameMenuPanel.SetActive(false);            */

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

//            resolutionDropdown = GameObject.Find("GameMenuPanel/OptionsMenu/ResolutionDropDown").GetComponent<TMP_Dropdown>();
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