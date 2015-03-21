using UnityEngine;

namespace Assets.Code.TileMaps.Entities
{
    // TileMap.cs - A single tile map, a collection of tiles
    public class TileMap : MonoBehaviour
    {
        // Key used for the map generation algorithm
        public int MapSeed { get; private set; }

        // Dimensions of the tile map measured in tiles
        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }

        // 2D array containing all of the tile data
        public Tile[,] Tiles { get; private set; }

        // Sets the initial data for the map
        public void SetMapData(int mapSeed, int mapWidth, int mapHeight, Tile[,] tiles)
        {
            this.MapSeed = mapSeed;
            this.MapWidth = mapWidth;
            this.MapHeight = mapHeight;
            this.Tiles = tiles;
        }

        // Checks if a position is within the map boundaries
        private bool IsPositionWithinMapBounds(int x, int y)
        {
            if (x <= MapWidth && x >= 0 && y <= MapHeight && y >= 0)
                return true;

            return false;
        }

        // Returns a tile by its position in the map
        public Tile GetTileByPosition(int x, int y)
        {
            if (IsPositionWithinMapBounds(x, y))
                return Tiles[x, y];

            // Return null if no tile matches the specified coordinates
            return null;
        }

        // Returns an array of tiles adjacent to the specified tile
        public Tile[] GetTileNeighbors(Tile tile)
        {
            // Get the coordinates of the specified tile
            int x = (int)tile.gameObject.transform.position.x;
            int y = (int)tile.gameObject.transform.position.y;
            // Return null if coordinates are not within map boundaries
            if (!IsPositionWithinMapBounds(x, y))
                return null;

            // Create an array of tile neighbors, ordered clockwise
            Tile[] neighbors = new Tile[4];
            // Up
            neighbors[0] = GetTileByPosition(x, y + 1);
            // Right
            neighbors[0] = GetTileByPosition(x + 1, y);
            // Down
            neighbors[0] = GetTileByPosition(x, y - 1);
            // Left
            neighbors[0] = GetTileByPosition(x - 1, y);

            return neighbors;
        }
    }
}