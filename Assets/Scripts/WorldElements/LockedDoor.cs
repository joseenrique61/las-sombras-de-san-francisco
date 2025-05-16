using UnityEngine;
using Inventory;
using Player.UI;
using Interactions;
using Player;

namespace WorldElements
{
    public class LockedDoor : MonoBehaviour, IInteractable
    {
        [SerializeField] private string requiredItemID = "UniqueItemID";
        private bool consumeItem = true;
        public bool isOpen = false;
        private GameObject doorVisual;
        private BoxCollider2D doorCollider;
        private InteractionAnimation anim;
        private InteractionObjectPrompt prompt;

        [Header("FX Sounds")]
        [SerializeField] private AudioClip openSound;
        [SerializeField] private AudioClip lockedSound;      // Sonido si está bloqueada
        
        public SoundsMannager soundsMannager;

        void Awake()
        {
            prompt = GetComponent<InteractionObjectPrompt>();
            doorCollider = GetComponent<BoxCollider2D>();
            anim = GetComponent<InteractionAnimation>();
        }

        void Start()
        {
            UpdateDoorState();

            doorVisual = gameObject;
        }

        public void AttemptOpen()
        {
            if (isOpen)
            {
                return;
            }

            if (InventorySystem.Instance != null && InventorySystem.Instance.HasItem(requiredItemID))
            {
                Debug.Log($"Abriendo puerta con {requiredItemID}.");
                isOpen = true;

                if (consumeItem)
                {
                    InventorySystem.Instance.RemoveItemById(requiredItemID);
                }

                // Aquí podrías también notificar a InteractionManager que la interacción fue exitosa
                soundsMannager.PlaySFX(openSound);
                UpdateDoorState();
                anim?.PlayOnceAnimation(); 
            }
            else
            {
                Debug.Log($"Falta el objeto: {requiredItemID}. Puerta bloqueada.");
                soundsMannager.PlaySFX(lockedSound);
            }
        }

        void UpdateDoorState()
        {
            if (doorVisual != null) doorVisual.SetActive(isOpen);
            if (doorCollider != null) doorCollider.enabled = !isOpen; 
        }

        public void Interact(GameObject player)
        {
            AttemptOpen();
        }

        public void ShowPrompt() => prompt?.ShowPrompt();
        public void HidePrompt() => prompt?.HidePrompt();

        public void OnEnterRange(GameObject player)
        {
            
        }

        public void OnExitRange(GameObject player)
        {

        }
    }
}