using Interactions;
using UnityEngine;

namespace Inventory
{
    [RequireComponent(typeof(Collider2D))]
    public class WorldItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemData itemData;
        private InteractionObjectPrompt prompt;
   
        private void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
            prompt = GetComponent<InteractionObjectPrompt>();
        }

        public void PickUpItem()
        {
            InventorySystem.Instance.AddItem(itemData);
            Destroy(gameObject);
        }

        public void Interact(GameObject interactor)
        {
            PickUpItem();
        }


        public void ShowPrompt() => prompt?.ShowPrompt();
        public void HidePrompt() => prompt?.HidePrompt();

        public void OnEnterRange(GameObject interactor)
        {
            Debug.Log("Puede agarrar la llave");
        }

        public void OnExitRange(GameObject interactor)
        {
            Debug.Log("Ya no puede agarrar la llave");
        }
    }
}