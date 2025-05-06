using UnityEngine;
using Inventory;
using Player.UI;

namespace Interactions
{
    public class LockedDoor : MonoBehaviour
    {
        private string requiredItemID = "UniqueItemID";
        private bool consumeItem = true;
        public bool isOpen = false;

        [Header("FeedBack")]
        [SerializeField] private GameObject doorVisualClosed;
        [SerializeField] private GameObject doorVisualOpen;
        [SerializeField] private Collider2D doorCollider;
        [SerializeField] private AudioClip openSound;
        [SerializeField] private AudioClip lockedSound;      // Sonido si está bloqueada
        
        public InteractionAnimation doorAnimation;
        public SoundsMannager soundsMannager;

        void Start()
        {
            UpdateDoorState();
            doorAnimation = GetComponent<InteractionAnimation>();
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
                doorAnimation?.PlayOnceAnimation(); 
            }
            else
            {
                Debug.Log($"Falta el objeto: {requiredItemID}. Puerta bloqueada.");
                soundsMannager.PlaySFX(lockedSound);
                // Opcional: Mostrar un mensaje en la UI "Necesitas la llave X"
            }
        }

        void UpdateDoorState()
        {
            if (doorVisualClosed != null) doorVisualClosed.SetActive(!isOpen);
            if (doorVisualOpen != null) doorVisualOpen.SetActive(isOpen);
            if (doorCollider != null) doorCollider.enabled = !isOpen; 
        }
    }
}