using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class PanelUI : MonoBehaviour
    {
        [Header("UI Settings")]
        [SerializeField] bool shouldBeActive;
        [SerializeField] bool shouldStopGamePlay;
        private GameObject gamePanel;
        private bool pausedGameplay;

        public void Start()
        {
            gamePanel = gameObject;

            if (gamePanel != null)
            {
                if (shouldBeActive) gamePanel.SetActive(true);
                else gamePanel.SetActive(false);
            }
            
            pausedGameplay = false;
        }

        public void PauseGameplay()
        {
            pausedGameplay = true;
            Time.timeScale = 0f;
        }

        public void ContinueGameplay()
        {
            pausedGameplay = false;
            Time.timeScale = 1f;
        }
        public void TogglePanel()
        {
            if (shouldStopGamePlay){
                if(gamePanel != null && !pausedGameplay)
                {
                    Debug.Log("The game has been paused");
                    PauseGameplay();
                    gamePanel.SetActive(!gamePanel.activeSelf);
                    return;
                }
                
                if(gamePanel != null && pausedGameplay)
                {
                    Debug.Log("The game has been reanuded");
                    ContinueGameplay();
                    gamePanel.SetActive(!gamePanel.activeSelf);
                    return;
                }
            }
            else
            {
                if(gamePanel != null)
                {
                    gamePanel.SetActive(!gamePanel.activeSelf);
                    return;
                }
            }
        }

        public void TogglePanel(InputAction.CallbackContext callbackContext)
        {
            if (!callbackContext.started) return;

            if (shouldStopGamePlay){
                if(gamePanel != null && !pausedGameplay)
                {
                    Debug.Log("The game has been paused");
                    PauseGameplay();
                    gamePanel.SetActive(!gamePanel.activeSelf);
                    return;
                }
                
                if(gamePanel != null && pausedGameplay)
                {
                    Debug.Log("The game has been reanuded");
                    ContinueGameplay();
                    gamePanel.SetActive(!gamePanel.activeSelf);
                    return;
                }
            }
            else
            {
                if(gamePanel != null)
                {
                    gamePanel.SetActive(!gamePanel.activeSelf);
                    return;
                }
            }
        }
    }
}