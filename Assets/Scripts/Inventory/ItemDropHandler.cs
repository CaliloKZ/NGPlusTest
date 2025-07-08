using System;
using GameEvents;
using Items;
using Pool;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Inventory
{
    public class ItemDropHandler : MonoBehaviour, IGameEventListener<InventorySlot>
    {
        [SerializeField] GameEvent<InventorySlot> onItemDrop;
        [SerializeField] float minDropDistance = 0.5f;
        [SerializeField] float maxDropDistance = 1.5f;

        void Start()
        {
            onItemDrop.RegisterListener(this);
        }

        public void OnEventRaised(InventorySlot source)
        {
            Vector2 dropOffset = Random.insideUnitCircle.normalized * Random.Range(minDropDistance, maxDropDistance);
            Vector2 dropPosition = new Vector2(transform.position.x, transform.position.y) + dropOffset;
            
            ItemCollectable item = FastPool.Instantiate<ItemCollectable>(source.ItemData.prefabID, dropPosition, quaternion.identity);
            item.SetItemAmount(source.StackSize);
        }
    }
}
