using Pool;
using UnityEngine;

namespace Items.Weapons
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] Rigidbody2D arrowRigidbody;
        [SerializeField] float speed = 10f;
        [SerializeField] string hitParticleEffectID;

        void FixedUpdate()
        {
            arrowRigidbody.linearVelocity = transform.right * speed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            FastPool.Instantiate(hitParticleEffectID, transform.position, Quaternion.identity);
            FastPool.Destroy(gameObject);
        }
    }
}
