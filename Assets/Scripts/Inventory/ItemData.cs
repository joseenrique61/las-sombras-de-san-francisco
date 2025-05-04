using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "New ItemData", menuName = "Inventory/Item Data")]
    public class ItemData : ScriptableObject
    {
        [Header("Info")]
        public string itemID = "UniqueItemID"; // ID único para identificar el objeto (ej: "llave_roja", "vela_inventario")
        public string displayName = "New Item";
        public string description = "Item Description";
        public Sprite icon; // Ícono para mostrar en la UI del inventario

        [Header("Gameplay")]
        public bool stackable = false; // ¿Se pueden apilar varios? (No necesario para llaves únicas)
        public bool usable = false;    // ¿Se puede "usar" desde el inventario?
        public bool keyItem = true;   // ¿Es un objeto clave (como para puertas)?
    }
}