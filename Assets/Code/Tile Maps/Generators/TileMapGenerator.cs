using Assets.Code.Entities;
using UnityEngine;

namespace Assets.Code.Generators
{
    public class TileMapGenerator : MonoBehaviour
    {
        // Singleton declaration
        private static TileMapGenerator instance;
        public static TileMapGenerator Instance
        {
            get
            {
                if (instance == null)
                    instance = GameObject.FindObjectOfType<TileMapGenerator>();
                return instance;
            }
        }

        void Start()
        {
            DestroyMap();
            GenerateMap(1, 10, 10);
        }

        // Destroys the tile map gameobject and all tile gameobjects associated with it
        private void DestroyMap()
        {
            GameObject[] tileMapObjects = GameObject.FindGameObjectsWithTag("TileMap");
            GameObject[] tileObjects = GameObject.FindGameObjectsWithTag("Tile");

            foreach (GameObject tileObject in tileObjects)
                DestroyImmediate(tileObject);

            foreach (GameObject tileMapObject in tileMapObjects)
                DestroyImmediate(tileMapObject);
        }

        // Generates a tile map gameobject along with the associated tile gameobjects
        private void GenerateMap(int mapSeed, int mapWidth, int mapHeight)
        {
            // Create tile map object and set its position to zero
            GameObject tileMapObject = new GameObject("Tile Map");
            tileMapObject.transform.position = Vector3.zero;

            // Create tile map and array of tiles
            TileMap tileMap = tileMapObject.AddComponent<TileMap>();
            Tile[,] tiles = new Tile[mapWidth, mapHeight];

            // Initialize tile selector
            TileSelector tileSelector = new TileSelector();

            // Loop through each position in the tile map and create tile object
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    // Get tile prefab from the tile selector within the given parameters
                    GameObject tilePrefab = tileSelector.GetTilePrefab(0);
                    // Initialize tile object from the prefab
                    GameObject tileObject = GameObject.Instantiate(tilePrefab) as GameObject;
                    // Get the tile data from the prefab
                    Tile tile = tileObject.GetComponent<Tile>();

                    // Set tile object name, parent, and position within the tile map
                    tileObject.name = tilePrefab.name + " (" + x + ", " + y + ")";
                    tileObject.transform.parent = tileMapObject.transform;
                    tileObject.transform.position = new Vector3(x, y, 0);

                    // Update the tiles array
                    tiles[x, y] = tile;
                }
            }

            // Set tile map data
            tileMap.SetMapData(mapSeed, mapWidth, mapHeight, tiles);
        }
    }
}