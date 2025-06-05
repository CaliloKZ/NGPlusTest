using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "Item_SO", menuName = "Scriptable Objects/Item_SO")]
    public class Item_SO : ScriptableObject
    {
        public int itemID;
        public string prefabID;
        
        public Sprite itemIcon;
        public string itemName;
        public string itemDescription;
        
        public bool isStackable;
        public int maxStackSize = 99;
    }
}
