using UnityEngine;
using Inventory;

namespace Interactions
{
    [RequireComponent(typeof(Collider2D))]
    public class WorldItem : MonoBehaviour
    {
        public ItemData itemData;
        
        private void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        public void PickUpItem()
        {
            Destroy(gameObject);
        }

    /*
        private void OnTriggerEnter2D(Collider2D collision) {
            
            if (collision.CompareTag("Player")) {
            
                if (InventoryManager.Instance != null && itemData != null) {

                    bool added = InventoryManager.Instance.AddItem(itemData);
                    if (added) {
                        Debug.Log($"Objeto {itemData.itemID} recogido");
                        Destroy(gameObject);
                    }
                    else
                        Debug.Log($"No fue posible recoger el objeto {itemData.itemID}");
                }
                else
                    Debug.LogError("InventoryManager no encontrado o ItemData no asignado en " + gameObject.name);            
            }
        }*/
    }
}