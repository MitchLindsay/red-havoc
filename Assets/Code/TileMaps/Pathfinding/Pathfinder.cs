using Assets.Code.TileMaps.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.TileMaps.Pathfinding
{
    public class Pathfinder
    {
        private TileMap TileMap;
        private List<Tile> open;
        private HashSet<Tile> closed;
        private Tile current;
        private Tile[] neighbors;

        public Pathfinder(TileMap tileMap)
        {
            this.TileMap = tileMap;
            this.open = new List<Tile>();
            this.closed = new HashSet<Tile>();
            this.current = null;
            this.neighbors = null;
        }

        public HashSet<Tile> GetTraversableRegion(Tile start, int range)
        {
            open = new List<Tile>();
            closed = new HashSet<Tile>();

            if (TileMap == null || start == null || range <= 0)
                return null;

            start.PathfindingNode.ResetData();

            open.Add(start);
            closed.Add(start);

            while (open.Count > 0)
            {
                current = open[0];
                open.Remove(current);

                neighbors = TileMap.GetTileNeighbors(current);
                foreach (Tile neighbor in neighbors)
                {
                    if (neighbor == null || closed.Contains(neighbor))
                        continue;

                    neighbor.PathfindingNode.CostFromStart = current.PathfindingNode.CostFromStart + Mathf.Abs(neighbor.MovementBonus);

                    if (neighbor.PathfindingNode.CostFromStart < range)
                    {
                        closed.Add(neighbor);
                        open.Add(neighbor);
                    }
                }
            }

            return closed;
        }
    }
}