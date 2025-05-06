using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory
{
    public class InventorySystem : MonoBehaviour
    {
        private Dictionary<ItemData, InventoryItem> _itemDictionary;
        public List<InventoryItem> inventoryItems;
        public static InventorySystem Instance;

        public delegate void OnInventoryChangedEvent();
        public event OnInventoryChangedEvent OnInventoryChangedEventCallBack;
        
        public void Awake()
        {
            inventoryItems = new List<InventoryItem>();
            _itemDictionary = new Dictionary<ItemData, InventoryItem>();           
            Instance = this;
        }

         public void AddItem (ItemData itemToAdd){
            if (itemToAdd==null)
            {
                Debug.LogError($"There is no an valid object");
                return;
            }


            if (_itemDictionary.TryGetValue(itemToAdd, out InventoryItem value))
            {
                value.AddStack();
                OnInventoryChangedEventCallBack.Invoke();
                Debug.Log($"Adding a new item to the Inventory with name {value.data.displayName} and quantity: {value.stackSize}");
            }
            else
            {
                InventoryItem newItem = new InventoryItem(itemToAdd);
                inventoryItems.Add(newItem);
                _itemDictionary.Add(itemToAdd,newItem);
                Debug.Log($"Adding a new item to the Inventory with name {newItem.data.displayName} and quantity: {newItem.stackSize}");
                OnInventoryChangedEventCallBack.Invoke();
            }

        }

        public void RemoveItem (ItemData itemToRemove){
            if (itemToRemove==null)
                 return;

            if (_itemDictionary.TryGetValue(itemToRemove, out InventoryItem value))
            {
                value.RemoveFromStack();
                if (value.stackSize == 0)
                {
                    inventoryItems.Remove(value);
                    _itemDictionary.Remove(itemToRemove);
                    OnInventoryChangedEventCallBack.Invoke();
                }
            }
        }

        public void RemoveItemById (string idItemToRemove){
            if (idItemToRemove==null)
                return;

            List<ItemData> data = new List<ItemData>(_itemDictionary.Keys);            
            ItemData itemToRemove = data.Find(i => i.itemID == idItemToRemove);

            RemoveItem(itemToRemove);
        }

        public bool HasItem(string itemIDToCheck)
        {
            foreach(InventoryItem item in inventoryItems)
            {
                if (item.data.itemID == itemIDToCheck)
                    return true;
            }

            return false;
        }
    }
}