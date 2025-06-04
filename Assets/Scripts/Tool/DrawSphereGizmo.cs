#if UNITY_EDITOR
using UnityEngine;

namespace Tool
{
    public class DrawSphereGizmo : MonoBehaviour
    {
        public Color color;
        public bool hasCustomPosition;
        public Transform customPosition;

        public float radius;
        void OnDrawGizmosSelected ()
        {
            Gizmos.color = color;
            Vector2 pos = hasCustomPosition ? customPosition.position : transform.position;
            Gizmos.DrawWireSphere(pos, radius);
        }
    }
}
#endif
