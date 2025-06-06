using UnityEngine;

namespace Items
{
    public class ItemRespawnBehaviour : MonoBehaviour
    {
        [SerializeField] float respawnDelay = 2f;

        void OnDisable()
        {
            Invoke(nameof(Reactivate), respawnDelay);
        }

        void Reactivate()
        {
            gameObject.SetActive(true);
        }
    }
}