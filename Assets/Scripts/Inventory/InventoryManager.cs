using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {

        [Header("UI Settings")]
        //[SerializeField] private GameObject InventoryPanel;
        [SerializeField] private List<Image> uiSlots;
        public List<ItemData> inventoryItems = new List<ItemData>();
        public UnityEvent onInventoryChanged;

        // Singleton Pattern
        public static InventoryManager Instance {get; private set;}
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public bool AddItem (ItemData itemToAdd){
            if (itemToAdd==null)
                return false;

            if (HasItem(itemToAdd.itemID) && itemToAdd.stackable)
                return false;

            inventoryItems.Add(itemToAdd);
            onInventoryChanged?.Invoke();

            return true;
        }

        public bool RemoveItem (string idItemToRemove){
            ItemData itemToRemove = inventoryItems.Find(i => i.itemID == idItemToRemove);

            if (itemToRemove==null)
            {
                inventoryItems.Remove(itemToRemove);
                onInventoryChanged?.Invoke();
                return true;
            }
            else
                return false;
        }

        public bool HasItem(string itemIDToCheck)
        {
            return inventoryItems.Exists(item => item.itemID == itemIDToCheck);
        }

        public bool ConsumeKeyItem(string itemIDToCheck)
        {
            if (HasItem(itemIDToCheck)) return RemoveItem(itemIDToCheck);
            
            return false;
        }


        private void Start() 
        {
            onInventoryChanged.AddListener(UpdateInventoryUI);
            UpdateInventoryUI();
        }

        void UpdateInventoryUI()
        {
            if (uiSlots == null || uiSlots.Count == 0) return; 

            for (int i = 0; i < uiSlots.Count; i++)
            {
                if (i < inventoryItems.Count && inventoryItems[i] != null)
                {
                    uiSlots[i].sprite = inventoryItems[i].icon;
                    uiSlots[i].enabled = true; 
                    uiSlots[i].color = Color.white;
                }
                else
                {
                    //uiSlots[i].sprite = null;
                    //uiSlots[i].enabled = false; // Oculta la imagen si no hay objeto
                    uiSlots[i].color = new Color(1,1,1, 0.5f); // Semitransparente
                    uiSlots[i].enabled = true; // Mantener visible pero vacío
                }
            }
        }

        // Método para mostrar/ocultar el inventario (puedes llamarlo con un botón)

    }
}