using UnityEngine;
using Inventory;
using Audio;
using Interactions;

namespace WorldElements
{
    public class Door : MonoBehaviour, IInteractable
    {
        [Header("Basic Door Configuration")]
        [SerializeField] private string requiredItemID = "UniqueItemID";
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

            if (InventorySystem.Instance != null && InventorySystem.Instance.HasItem(requiredItemID))
            {
                if (consumeItem)
                {
                    InventorySystem.Instance.RemoveItemById(requiredItemID);
                }

                Debug.Log($"Abriendo puerta con {requiredItemID}.");
                isOpen = true;
                tryToOpen = false;
                audioController.PlaySFX(openSound);
                UpdateDoorState();
                
                anim?.PlayOnceAnimation();
            }
            else
            {
                Debug.Log($"Falta el objeto: {requiredItemID}. Puerta bloqueada.");
                isOpen = false;
                tryToOpen = true;
                audioController.PlaySFX(lockedSound);
                UpdateDoorState();
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