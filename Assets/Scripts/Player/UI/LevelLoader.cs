using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Inventory;

namespace Player.UI
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float animationTime = 1f;

        public void LoadNextLevel()
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }

        public void RestartLevel()
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        }

        public void GoToMainMenu()
        {
            StartCoroutine(LoadLevel(0));
        }

        public void ExitGame()
        {
            Debug.Log("The game has been quited...");
            Application.Quit();
        }

        private IEnumerator LoadLevel(int levelId)
        {
            animator.SetTrigger("Activate");

            yield return new WaitForSeconds(animationTime);

            SceneManager.LoadScene(levelId);
        }
    }
}