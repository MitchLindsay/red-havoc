using Assets.Code.Entities.TileMaps;
using Assets.Code.Generic.GameObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Code.Entities.Pathfinding
{
    public class Pathfinder : Map<PathfindingNode>
    {
        private Queue<PathfindingNode> open;
        private HashSet<PathfindingNode> closed;
        private PathfindingNode current;

        public override void InitializeMap(int width, int height, PathfindingNode[,] data)
        {
            this.open = new Queue<PathfindingNode>();
            this.closed = new HashSet<PathfindingNode>();
            this.current = null;

            base.InitializeMap(width, height, data);
        }

        public HashSet<PathfindingNode> GetRegion(PathfindingNode start, int range)
        {
            open = new Queue<PathfindingNode>();
            closed = new HashSet<PathfindingNode>();

            if (Data == null || start == null || range <= 0)
                return null;

            start.ResetData();
            open.Enqueue(start);
            closed.Add(start);

            PathfindingNode[] neighbors = null;
            while (open.Any())
            {
                current = open.Dequeue();
                neighbors = GetDataNeighbors(current);
                foreach (PathfindingNode neighbor in neighbors)
                {
                    if (neighbor == null || closed.Contains(neighbor))
                        continue;

                    neighbor.CostFromStart = current.CostFromStart + neighbor.MovementCost;
                    if (neighbor.CostFromStart < range)
                    {
                        closed.Add(neighbor);
                        open.Enqueue(neighbor);
                    }
                }
            }

            return closed;
        }
    }
}