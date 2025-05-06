using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryUIController : MonoBehaviour
    {
        [Header("UI Settings")]
        [SerializeField] private List<Image> uiSlots;

        private void Start()
        {
            
        }

        private void OnDestroy()
        {
          
        }

        void UpdateInventoryUI()
        {
            if (InventorySystem.Instance == null)
            {
                Debug.LogError("Intentando actualizar UI pero InventoryManager.Instance es null.");
                return;
            }
            
            /*List<ItemData> inventoryItems = InventorySystem.Instance.inventoryItems;

            if (uiSlots == null || uiSlots.Count == 0)
            {
                Debug.LogWarning("uiSlots no asignado o vacío en InventoryUIController.");
                return;
            }

            for (int i = 0; i < uiSlots.Count; i++)
            {
                if (uiSlots[i] == null) continue;

                if (i < inventoryItems.Count && inventoryItems[i] != null)
                {
                    uiSlots[i].sprite = inventoryItems[i].icon;
                    uiSlots[i].enabled = true;
                    uiSlots[i].color = Color.white;
                }
                else
                {
                    uiSlots[i].sprite = null; // Poner sprite transparente o por defecto es mejor
                    uiSlots[i].enabled = true; // Mantener visible pero vacío
                }
            }*/
            Debug.Log("Inventory UI actualizada.");
        }
    }
}