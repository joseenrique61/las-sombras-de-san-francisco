using UnityEngine;

namespace Interactions
{
    public class InteractionObjectPrompt : MonoBehaviour
    {
        public GameObject InteractionPrompt;
        public void ShowPrompt() {
            InteractionPrompt.SetActive(true);
        }

        public void HidePrompt() {
            InteractionPrompt.SetActive(false);
        }
    }
}