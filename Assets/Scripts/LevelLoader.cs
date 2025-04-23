using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private IEnumerator LoadLevel(int levelId)
    {
        animator.SetTrigger("Activate");

        yield return new WaitForSeconds(animationTime);

        SceneManager.LoadScene(levelId);
    }
}
