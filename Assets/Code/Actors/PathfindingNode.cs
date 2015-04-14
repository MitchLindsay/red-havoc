using Assets.Code.Controllers;
using UnityEngine;

namespace Assets.Code.Actors
{
    public class PathfindingNode : Actor
    {
        public int MoveCost { get; private set; }
        public bool Traversable { get; private set; }

        void Start()
        {
            CheckForCollisions<Tile>(layerMasks.TileMask);
            CheckForCollisions<Unit>(layerMasks.UnitMask);
        }

        public override void HandleCollision<T>(GameObject collidedObject)
        {
            if (collidedObject != null)
            {
                // Handle Tile Collisions
                if (typeof(T) == typeof(Tile))
                    ApplyTileMoveCost(collidedObject.GetComponent<Tile>());

                // Handle Unit Collisions
                if (typeof(T) == typeof(Unit))
                    Traversable = false;
            }
            else
                Traversable = true;
        }

        private void ApplyTileMoveCost(Tile tile)
        {
            if (tile != null)
                MoveCost = tile.MovementCost;
        }
    }
}