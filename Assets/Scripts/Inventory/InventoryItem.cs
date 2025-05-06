namespace Inventory
{
    public class InventoryItem
    {
        public ItemData data;
        public int stackSize;
        public InventoryItem (ItemData _data)
        {
            data = _data;
            AddStack();
        }

        public void AddStack()
        {
            if (data.stackable)
                stackSize++;
            else 
                stackSize = 1;
        }

        public void RemoveFromStack()
        {
            stackSize--;
        }
    }
}
