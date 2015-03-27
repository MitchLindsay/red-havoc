using Assets.Code.GUI.WorldSpace;
using Assets.Code.TileMaps.Pathfinding;
using UnityEngine;

namespace Assets.Code.TileMaps.Entities
{
    public class TileMap : MonoBehaviour
    {
        public int MapSeed { get; private set; }
        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }
        public Tile[,] Tiles { get; private set; }
        public Pathfinder Pathfinder { get; private set; }

        public void SetMapData(int mapSeed, int mapWidth, int mapHeight, Tile[,] tiles)
        {
            this.MapSeed = mapSeed;
            this.MapWidth = mapWidth;
            this.MapHeight = mapHeight;
            this.Tiles = tiles;
            this.Pathfinder = new Pathfinder(this);
        }

        private bool IsPositionWithinMapBounds(int x, int y)
        {
            if (x <= MapWidth && x >= 0 && y <= MapHeight && y >= 0)
                return true;

            return false;
        }

        public Tile GetTileByPosition(int x, int y)
        {
            if (IsPositionWithinMapBounds(x, y))
                return Tiles[x, y];

            return null;
        }

        public Tile[] GetTileNeighbors(Tile tile)
        {
            int x = (int)tile.gameObject.transform.position.x;
            int y = (int)tile.gameObject.transform.position.y;

            if (!IsPositionWithinMapBounds(x, y))
                return null;

            Tile[] neighbors = new Tile[4];
            neighbors[0] = GetTileByPosition(x, y + 1); // Up
            neighbors[1] = GetTileByPosition(x + 1, y); // Right
            neighbors[2] = GetTileByPosition(x, y - 1); // Down
            neighbors[3] = GetTileByPosition(x - 1, y); // Left

            return neighbors;
        }
    }
}