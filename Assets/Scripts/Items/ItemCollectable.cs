using GameEvents;
using Inventory;
using UnityEngine;

namespace Items
{
    public class ItemCollectable : MonoBehaviour
    {
        const string PlayerTag = "Player";
        public int Amount { get; private set; } = 1;
        public Item_SO itemData;
        
        [SerializeField] GameEvent<MonoBehaviour> onItemPickup;

        public void SetItemAmount(int newAmount)
        {
            Amount = newAmount;
        }

        public void ItemCollected()
        {
            gameObject.SetActive(false);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(PlayerTag)) 
                return;
        
            onItemPickup.Raise(this);
        }
    }
}
