using UnityEngine;

namespace Assets.Code.Entities
{
    public class TileMap : MonoBehaviour
    {
        // Map Seed, assigned when Tiles are generated
        private int MapSeed = 0;

        // Map dimensions, measured in Tiles
        public int MapWidth = 10;
        public int MapHeight = 10;

        // Array containing all Tile data
        private Tile[,] tiles;

        void Start()
        {
            DestroyTiles();
            GenerateTiles();
        }

        // MOVE TO OTHER CLASSES ////////////////////////////////////////////////////////////////////////////////////////////////

        // Destroys any previously generated Tiles
        private void DestroyTiles()
        {
            GameObject[] tileObjects = GameObject.FindGameObjectsWithTag("Tile");

            foreach (GameObject tileObject in tileObjects)
                DestroyImmediate(tileObject);
        }

        // Generates a new set of Tiles
        private void GenerateTiles()
        {
            tiles = new Tile[MapWidth, MapHeight];

            // Placeholder tile
            GameObject tilePrefab = Resources.Load("Tiles/Tile_Plains", typeof(GameObject)) as GameObject;

            for (int y = 0; y < MapHeight; y++)
                for (int x = 0; x < MapWidth; x++)
                    InstantiateTilePrefab(tilePrefab, x, y);
        }

        // Instantiates a Tile GameObject at the specified location
        private void InstantiateTilePrefab(GameObject tilePrefab, int x, int y)
        {
            if (tilePrefab != null && IsPositionWithinMapBounds(x, y))
            {
                GameObject objectInstance = GameObject.Instantiate(tilePrefab) as GameObject;
                Tile tileInstance = gameObject.GetComponent<Tile>();

                objectInstance.name = tilePrefab.name + " (" + x + ", " + y + ")";
                objectInstance.transform.parent = gameObject.transform;
                objectInstance.transform.position = new Vector3(x, y, 0);

                tiles[x, y] = tileInstance;
            }
        }

        // END MOVE ////////////////////////////////////////////////////////////////////////////////////////////////////

        // Checks if a position is within the Tile Map boundaries
        private bool IsPositionWithinMapBounds(int x, int y)
        {
            if (x <= MapWidth && x >= 0 && y <= MapHeight && y >= 0)
                return true;

            return false;
        }

        // Returns Tile by its Tile ID
        public Tile GetTileByTileID(int tileID)
        {
            // Return null if Tile ID is not set
            if (tileID == -1)
                return null;

            foreach (Tile tile in tiles)
            {
                if (tile.TileID == tileID)
                    return tile;
            }

            return null;
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