using UnityEngine;

namespace Assets.Code.Entities
{
    public class TileMap : MonoBehaviour
    {
        // Map Seed - Assigned by Tile Map Generator
        private int mapSeed;

        // Map Dimensions (in Tiles)
        public int MapWidth = 10;
        public int MapHeight = 10;

        // Array containing all Tiles in the Tile Map
        private Tile[,] tiles;

        void Start()
        {
            DestroyTiles();
            GenerateTiles();
        }

        private void DestroyTiles()
        {

        }

        private void GenerateTiles()
        {

        }

        // Checks if a position is within the Tile Map boundaries
        private bool IsPositionWithinMapBounds(int x, int y)
        {
            if (x <= MapWidth && x >= 0 && y <= MapHeight && y >= 0)
                return true;

            return false;
        }

        // Returns Tile by its position in the Tile Map
        public Tile GetTileByPosition(int x, int y)
        {
            if (IsPositionWithinMapBounds(x, y))
                return tiles[x, y];

            return null;
        }

        // Returns an array of Tiles adjacent to the specified Tile
        public Tile[] GetTileNeighbors(Tile tile)
        {
            // Get the coordinates of the specified Tile
            int x = (int)tile.gameObject.transform.position.x;
            int y = (int)tile.gameObject.transform.position.y;

            // If coordinates are not within Tile Map boundaries, return nothing
            if (!IsPositionWithinMapBounds(x, y))
                return null;

            // Create an array of Tile neighbors, ordered clockwise
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