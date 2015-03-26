using UnityEngine;

namespace Assets.Code.Generic
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class Entity : MonoBehaviour
    {
        public string EntityName = "Entity";

        [HideInInspector]
        public bool CollisionsEnabled = false;
        public LayerMask TileLayerMask = 0;
        public LayerMask UnitLayerMask = 0;

        private Vector3 raycastOffset = new Vector3(0.5f, 0.0f, 0.0f);

        private RaycastHit2D raycastHit;

        public void CheckForCollisions<TEntity>(LayerMask layerMask)
        {
            Debug.DrawRay(transform.position + raycastOffset, Vector2.up, Color.red);
            raycastHit = Physics2D.Raycast(transform.position + raycastOffset, Vector2.up, 1.0f, layerMask);

            if (raycastHit.collider != null)
                HandleCollision<TEntity>(raycastHit.collider.gameObject);
            else
                HandleCollision<TEntity>(null);
        }

        public virtual void HandleCollision<TEntity>(GameObject collidedGameObject) { }

        public virtual void EnableCollisions()
        {
            CollisionsEnabled = true;
        }

        public virtual void DisableCollisions()
        {
            CollisionsEnabled = false;
        }
    }
}