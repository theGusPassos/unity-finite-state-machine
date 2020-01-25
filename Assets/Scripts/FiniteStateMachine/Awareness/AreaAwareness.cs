using UnityEngine;

namespace Assets.Scripts.FiniteStateMachine.Awareness
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class AreaAwareness : MonoBehaviour, IAwareness
    {
        private Transform target;

        public bool HasTarget() => target != null;

        public int GetTargetDirection()
        {
            if (!HasTarget()) return int.MaxValue;
            return target.position.x > transform.position.x ? 1 : -1;
        }

        public float GetTargetDistance() => Vector2.Distance(transform.position, target.position);

        public float GetTargetHorizontalDistance() 
            => Mathf.Abs(transform.position.x - target.position.x);

        private void OnTriggerEnter2D(Collider2D collision)
        {
            target = collision.transform;
        }

        public T GetComponentFromTarget<T>() => target.GetComponent<T>();

        public void ResetTarget() => target = null;
    }
}
