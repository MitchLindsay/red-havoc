using Assets.Code.Entities.Abstract;
using Assets.Code.Entities.Tiles;
using Assets.Code.Entities.Units;
using System;
using UnityEngine;

namespace Assets.Code.Entities.Pathfinding
{
    [RequireComponent (typeof(SpriteRenderer))]
    [RequireComponent (typeof(BoxCollider2D))]
    public class Node : Entity
    {
        public int MoveCost = 0;
        public bool Traversable = true;
        public bool CanBeGoal = true;

        void Start()
        {
            CheckForCollisions<Tile>(TileLayerMask);
            CheckForCollisions<Unit>(UnitLayerMask);
        }

        public void SetColor(Color color)
        {
            gameObject.GetComponent<SpriteRenderer>().color = color;
        }

        public override void HandleCollision<TEntity>(GameObject collidedGameObject)
        {
            if (typeof(TEntity) == typeof(Tile) && collidedGameObject != null)
            {
                Tile tile = collidedGameObject.GetComponent<Tile>();
                MoveCost = 1 * Mathf.Abs(tile.MovementBonus);

                if (MoveCost < 1)
                    MoveCost = 1;
            }

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