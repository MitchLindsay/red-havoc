using Assets.Code.Entities.Abstract;
using Assets.Code.Entities.Units;
using UnityEngine;

namespace Assets.Code.Entities.Pathfinding
{
    [RequireComponent (typeof(SpriteRenderer))]
    [RequireComponent (typeof(BoxCollider2D))]
    public class Node : Entity
    {
        public Color32 ColorTraversable = new Color32(61, 118, 229, 200);
        public Color32 ColorOutOfReach = new Color32(229, 61, 61, 200);
        public Color32 ColorNeutral = new Color32(255, 255, 255, 0);

        public int MoveCost = 0;
        public bool Traversable = true;
        public bool CanBeGoal = true;

        void Start()
        {
            SetColor(ColorNeutral);
        }

        void Update()
        {
            CheckForCollisions<Unit>(UnitLayerMask);
        }

        private void SetColor(Color color)
        {
            gameObject.GetComponent<SpriteRenderer>().color = color;
        }

        public override void HandleCollision<TEntity>(GameObject collidedGameObject)
        {
            if (typeof(TEntity) == typeof(Unit) && collidedGameObject != null)
            {
                CanBeGoal = false;
                Traversable = false;
            }
            else
            {
                CanBeGoal = true;
                Traversable = true;
            }
        }
    }
}