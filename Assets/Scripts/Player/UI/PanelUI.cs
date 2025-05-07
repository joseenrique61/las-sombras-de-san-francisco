using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class PanelUI : MonoBehaviour
    {
        [Header("UI Settings")]
        [SerializeField] GameObject GamePanel;
        [SerializeField] bool shouldStopGamePlay;
        private bool PausedGameplay;

        public void Start()
        {
            if(GamePanel != null) 
                GamePanel.SetActive(false);
            PausedGameplay = false;
        }

        public void PauseGameplay()
        {
            PausedGameplay = true;
            Time.timeScale = 0f;
        }

        public void ContinueGameplay()
        {
            PausedGameplay = false;
            Time.timeScale = 1f;
        }
        public void TogglePanel()
        {
            if (shouldStopGamePlay == true){
                if(GamePanel != null && PausedGameplay==false)
                {
                    Debug.Log("The game has been paused");
                    PauseGameplay();
                    GamePanel.SetActive(!GamePanel.activeSelf);
                    return;
                }
                
                if(GamePanel != null && PausedGameplay==true)
                {
                    Debug.Log("The game has been reanuded");
                    ContinueGameplay();
                    GamePanel.SetActive(!GamePanel.activeSelf);
                    return;
                }
            }
            else
            {
                if(GamePanel != null)
                {
                    GamePanel.SetActive(!GamePanel.activeSelf);
                    return;
                }
            }
        }

        public void TogglePanel(InputAction.CallbackContext callbackContext)
        {
            if (!callbackContext.started) return;

            if (shouldStopGamePlay == true){
                if(GamePanel != null && PausedGameplay==false)
                {
                    Debug.Log("The game has been paused");
                    PauseGameplay();
                    GamePanel.SetActive(!GamePanel.activeSelf);
                    return;
                }
                
                if(GamePanel != null && PausedGameplay==true)
                {
                    Debug.Log("The game has been reanuded");
                    ContinueGameplay();
                    GamePanel.SetActive(!GamePanel.activeSelf);
                    return;
                }
            }
            else
            {
                if(GamePanel != null)
                {
                    GamePanel.SetActive(!GamePanel.activeSelf);
                    return;
                }
            }
        }
    }
}