using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Interactions;
using Player;

namespace WorldElements
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ClosetInteractable : MonoBehaviour, IInteractable
    {
        private InteractionObjectPrompt prompt;
        private InteractionAnimation anim;
        private BoxCollider2D closetCollider;

        private PlayerController playerController;
        private GameObject player;
        private Vector2 originalPosition;

        private GameObject playerVisuals;
        private GameObject playerCandle;
        private PlayerInput playerInput;

        private void Awake()
        {
            prompt = GetComponent<InteractionObjectPrompt>();
            anim = GetComponent<InteractionAnimation>();
            closetCollider = GetComponents<BoxCollider2D>().FirstOrDefault(x => !x.isTrigger);
        }

        public void ShowPrompt() => prompt?.ShowPrompt();
        public void HidePrompt() => prompt?.HidePrompt();

        public void OnEnterRange(GameObject interactor)
        {
            player = interactor;

            playerController = player.GetComponent<PlayerController>();
            playerVisuals = player.transform.Find("PlayerVisuals")?.gameObject;
            playerCandle = player.transform.Find("Candle")?.gameObject;
            playerInput = player.GetComponent<PlayerInput>();

            if (closetCollider == null)
            {
                Debug.LogWarning("Closet collider no encontrado.");
            }
        }

        public void OnExitRange(GameObject interactor)
        {
            if (!playerController.isHidden && interactor == player)
            {
                player = null;
                playerVisuals = null;
                playerCandle = null;
                playerInput = null;
            }
        }

        public void Interact(GameObject interactor)
        {
            if (interactor != player || closetCollider == null)
                return;

            anim?.PlayOnceAnimation();

            if (!playerController.isHidden)
            {
                originalPosition = player.transform.position;

                playerVisuals?.SetActive(false);
                playerCandle?.SetActive(false);
                playerInput?.actions["Move"].Disable();

                player.transform.position = closetCollider.transform.position;
                closetCollider.enabled = false;

                playerController.isHidden = true;
                Debug.Log("Jugador escondido");
            }
            else
            {
                playerVisuals?.SetActive(true);
                playerCandle?.SetActive(true);
                playerInput?.actions["Move"].Enable();

                player.transform.position = originalPosition;
                closetCollider.enabled = true;

                playerController.isHidden = false;
                Debug.Log("Jugador salió del closet");
            }
        }
    }
}