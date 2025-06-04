using Pool;
using UnityEngine;

namespace Items.Weapons
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] Rigidbody2D arrowRigidbody;
        [SerializeField] float speed = 10f;
        [SerializeField] string hitParticleEffectID;

        void Start()
        {
            if(null == arrowRigidbody)
                return;
        
            arrowRigidbody.linearVelocity = transform.up * speed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            FastPool.Instantiate(hitParticleEffectID, transform.position, Quaternion.identity);
            FastPool.Destroy(gameObject);
        }
    }
}
