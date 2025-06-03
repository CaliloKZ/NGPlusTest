using UI.Inventory;
using UnityEngine;

public class ItemCollectable : MonoBehaviour
{
    const string PlayerTag = "Player";
    public Item_SO itemData;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(PlayerTag)) 
            return;
        
        InventorySystem.OnItemPickupAction?.Invoke(this);
    }
}
