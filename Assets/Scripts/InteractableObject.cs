using UnityEngine;
public class InteractableObject : MonoBehaviour
{
    public GameObject InteractionPrompt;
    public void ShowPrompt() {
        InteractionPrompt.SetActive(true);
    }

    public void HidePrompt() {
        InteractionPrompt.SetActive(false);
    }
}