using Inventory;
using UnityEngine;

namespace Items
{
    public class ItemCollectable : MonoBehaviour
    {
        const string PlayerTag = "Player";
        public Item_SO itemData;
        public int amount = 1;

        public void SetItemAmount(int newAmount)
        {
            amount = newAmount;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(PlayerTag)) 
                return;
        
            InventorySystem.OnItemPickupAction?.Invoke(this);
        }
    }
}
