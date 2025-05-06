using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "New ItemData", menuName = "Inventory/Item Data/Create Item")]
    public class ItemData : ScriptableObject
    {
        [Header("Info")]
        public string itemID = "UniqueItemID";
        public string displayName = "New Item";
        public string description = "Item Description";
        public Sprite icon;
        //[SerializeField] public GameObject itemPrefab;

        [Header("Gameplay")]
        public bool stackable = false;
        public bool usable = false;
        public bool keyItem = true;
    }
}