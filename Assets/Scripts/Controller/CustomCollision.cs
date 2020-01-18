using UnityEngine;

namespace Assets.Scripts.Controller
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CustomCollision : MonoBehaviour
    {
        private BoxCollider2D boxCollider;
        public CollisionInfo info;

        private const float skinWidth = 0.015f;
        private const int verticalRayCount = 4;
        private const int horizontalRayCount = 4;
        private float horizontalRaySpacing;
        private float verticalRaySpacing;

        private RayOrigins rayOrigins = new RayOrigins();

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            CalculateRaySpacing();
        }

        private void CalculateRaySpacing()
        {
            Bounds bounds = boxCollider.bounds;
            bounds.Expand(-2 * skinWidth);

            horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
            verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
        }

        public void UpdateRayOrigins()
        {
            Bounds bounds = boxCollider.bounds;
            bounds.Expand(-2 * skinWidth);

            rayOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
            rayOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
            rayOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
            rayOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
        }

        public void HandleVerticalCollisions(ref Vector2 velocity, LayerMask collisionMask)
        {
            float direction = Mathf.Sign(velocity.y);
            float rayLength = Mathf.Abs(velocity.y) + skinWidth;

            for (int i = 0; i < horizontalRayCount; i++)
            {
                Vector2 rayOrigin = direction > 0
                    ? rayOrigins.topLeft
                    : rayOrigins.bottomLeft;

                rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * direction, rayLength, collisionMask);

                if (hit)
                {
                    velocity.y = (hit.distance - skinWidth) * direction;
                    rayLength = hit.distance;

                    if (direction > 0) info.above = true;
                    else info.bellow = true;
                }
            }
        }

        public void HandleHorizontalCollisions(ref Vector2 velocity, LayerMask collisionMask)
        {
            float direction = Mathf.Sign(velocity.x);
            float rayLength = Mathf.Abs(velocity.x) + skinWidth;

            for (int i = 0; i < verticalRayCount; i++)
            {
                Vector2 rayOrigin = direction > 0
                    ? rayOrigins.bottomRight
                    : rayOrigins.bottomLeft;

                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * direction, rayLength, collisionMask);

                if (hit)
                {
                    velocity.x = (hit.distance - skinWidth) * direction;
                    rayLength = hit.distance;
                }
            }
        }

        private struct RayOrigins
        {
            public Vector2 bottomLeft;
            public Vector2 bottomRight;
            public Vector2 topLeft;
            public Vector2 topRight;
        }

        public struct CollisionInfo
        {
            public bool bellow;
            public bool above;

            public void Reset()
            {
                bellow = above = false;
            }
        }
    }
}
