using UnityEngine;
using Inventory;
using Audio;
using Interactions;
using UnityEngine.UI;

namespace WorldElements
{
    public class Door : MonoBehaviour, IInteractable
    {
        [Header("Basic Door Configuration")]
        [SerializeField] private ItemData requiredItem;
        [SerializeField] private Image imageItem;
        private bool consumeItem = true;
        public bool isOpen = false;
        public bool tryToOpen = false;
        private BoxCollider2D doorCollider;
        private InteractionAnimation anim;
        private InteractionObjectPrompt prompt;
        private AudioController audioController;

        [Header("FX Sounds")]
        [SerializeField] private AudioClip openSound;
        [SerializeField] private AudioClip lockedSound;
        
        void Awake()
        {
            audioController = GetComponent<AudioController>();
            prompt = GetComponent<InteractionObjectPrompt>();
            doorCollider = GetComponent<BoxCollider2D>();
            anim = GetComponent<InteractionAnimation>();
        }

        void Start()
        {
            UpdateDoorState();
        }

        public void AttemptOpen()
        {
            if (isOpen)
            {
                return;
            }

            if (InventorySystem.Instance != null && InventorySystem.Instance.HasItem(requiredItem.itemID))
            {
                if (consumeItem)
                {
                    InventorySystem.Instance.RemoveItemById(requiredItem.itemID);
                }

                Debug.Log($"Abriendo puerta con {requiredItem.itemID}.");
                isOpen = true;
                tryToOpen = false;
                audioController.PlaySFX(openSound);
                UpdateDoorState();

                anim?.PlayOnceAnimation();
            }
            else
            {
                Debug.Log($"Falta el objeto: {requiredItem.itemID}. Puerta bloqueada.");
                isOpen = false;
                tryToOpen = true;
                audioController.PlaySFX(lockedSound);
                UpdateDoorState();

                imageItem.sprite = requiredItem.icon;
            }
        }

        void UpdateDoorState()
        {
            if (doorCollider != null) doorCollider.enabled = !isOpen;

            if (!isOpen && tryToOpen) prompt?.ShowPrompt("MessagePrompt");
            if (isOpen && !tryToOpen) prompt?.HidePrompt("MessagePrompt");
        }

        public void Interact(GameObject player)
        {
            AttemptOpen();
        }

        public void ShowPrompt() => prompt?.ShowPrompt();
        public void HidePrompt() => prompt?.HidePrompt();

        public void OnEnterRange(GameObject player)
        {
            if (!isOpen && tryToOpen) prompt?.ShowPrompt("MessagePrompt");
        }

        public void OnExitRange(GameObject player)
        {
            if (!isOpen && tryToOpen) prompt?.HidePrompt("MessagePrompt");
        }
    }
}