using UnityEngine;

namespace Assets.Scripts.Controller
{
    [RequireComponent(typeof(CustomCollision))]
    public class CharacterController : MonoBehaviour
    {
        private CustomCollision collision;
        private Vector2 currentVelocity;

        [SerializeField] private float maxJumpHeight;
        [SerializeField] private float minJumpDivisor;
        [SerializeField] private float timeToMaxJumpHeight;
        [SerializeField] private float moveSpeed;
        [SerializeField] private LayerMask collisionMask;

        private float gravity;
        private float jumpVelocity;
        private float minJumpVelocity;

        private float xInput;

        private void Awake()
        {
            collision = GetComponent<CustomCollision>();
            CalculateGravity();
        }

        [ContextMenu("Recalculate Gravity")]
        private void CalculateGravity()
        {
            gravity = (-2 * maxJumpHeight) / timeToMaxJumpHeight * 2;
            jumpVelocity = Mathf.Abs(gravity) * timeToMaxJumpHeight;
            minJumpVelocity = jumpVelocity / minJumpDivisor;
        }

        public void SetXInput(float xInput) => this.xInput = xInput;

        public void OnJumpButtonDown()
        {
            if (collision.info.bellow)
            {
                currentVelocity.y = jumpVelocity;
            }
        }

        public void OnJumpButtonUp()
        {
            if (currentVelocity.y > minJumpVelocity)
            {
                currentVelocity.y = minJumpVelocity;
            }
        }

        private void FixedUpdate()
        {
            currentVelocity.x = xInput * moveSpeed;
            currentVelocity.y += gravity * Time.fixedDeltaTime;

            Vector2 velocity = currentVelocity * Time.fixedDeltaTime;

            collision.UpdateRayOrigins();
            collision.info.Reset();

            if (velocity.x != 0)
            {
                collision.HandleHorizontalCollisions(ref velocity, collisionMask);
            }
            if (velocity.y != 0)
            {
                collision.HandleVerticalCollisions(ref velocity, collisionMask);
            }

            transform.Translate(velocity);

            if (collision.info.bellow || collision.info.above)
            {
                currentVelocity.y = 0;
            }
        }
    }
}
