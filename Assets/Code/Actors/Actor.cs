using UnityEngine;

namespace Assets.Code.Actors
{
    [RequireComponent (typeof(SpriteRenderer))]
    [RequireComponent (typeof(BoxCollider2D))]
    public abstract class Actor : MonoBehaviour
    {
        public string Name = "Actor";
        public LayerMask LayerMask = 0;
        public Vector3 RaycastOffset = new Vector3(0.5f, 0.0f, 0.0f);
        private RaycastHit2D raycastHit;

        public void CheckForCollisions<TActor>(LayerMask layerMaskToCheck)
        {
            Vector3 raycastPosition = transform.position + RaycastOffset;
            raycastHit = Physics2D.Raycast(raycastPosition, Vector2.up, 1.0f, layerMaskToCheck);
            
            if (raycastHit.collider != null)
                HandleCollision<TActor>(raycastHit.collider.gameObject);
            else
                HandleCollision<TActor>(null);
        }

        public virtual void HandleCollision<TActor>(GameObject collidedObject) { }
    }
}