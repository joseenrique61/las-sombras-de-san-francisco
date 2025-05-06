using UnityEngine;

namespace Inventory
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
    }
}