using Assets.Code.Controllers;
using UnityEngine;

namespace Assets.Code.Actors
{
    [RequireComponent (typeof(SpriteRenderer))]
    [RequireComponent (typeof(BoxCollider2D))]
    public abstract class Actor : MonoBehaviour
    {
        public string Name = "Actor";
        public Vector3 RaycastOffset = new Vector3(0.5f, 0.0f, 0.0f);
        public LayerMaskLibrary layerMasks;
        private RaycastHit2D raycastHit;

        void Awake()
        {
            if (layerMasks == null)
                layerMasks = LayerMaskLibrary.Instance;
        }

        public void CheckForCollisions<T>(LayerMask layerMaskToCheck)
        {
            Vector3 raycastPosition = transform.position + RaycastOffset;
            raycastHit = Physics2D.Raycast(raycastPosition, Vector2.up, 1.0f, layerMaskToCheck);
            
            if (raycastHit.collider != null)
                HandleCollision<T>(raycastHit.collider.gameObject);
            else
                HandleCollision<T>(null);
        }

        public virtual void HandleCollision<T>(GameObject collidedObject) { }
    }
}