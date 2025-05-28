using UnityEngine;
using UnityEngine.InputSystem;

namespace Interactions
{
    public class InteractionController : MonoBehaviour
    {
        private IInteractable currentInteractable;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var interactable = collision.GetComponent<IInteractable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
                currentInteractable.ShowPrompt();
                currentInteractable.OnEnterRange(gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var interactable = collision.GetComponent<IInteractable>();
            if (interactable != null && interactable == currentInteractable)
            {
                currentInteractable.HidePrompt();
                currentInteractable.OnExitRange(gameObject);
                currentInteractable = null;
            }
        }

        public void Interact(InputAction.CallbackContext callbackContext)
        {
            if (!callbackContext.started)
                return;

            if (currentInteractable != null)
            {
                currentInteractable.Interact(gameObject);
            }
        }
    }
}
