using Assets.Code.TileMaps.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.TileMaps.Generators
{
    // TileMapGenerator.cs - Generates tile maps
    public class TileMapGenerator : MonoBehaviour
    {
        // Event handler for when map generation complete
        public delegate void MapGenerationHandler(int width, int height);
        public static event MapGenerationHandler OnGenerationComplete;

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

        // Map seed, edited through Unity interface
        public int MapSeed = 1;
        // Map dimensions, edited through Unity interface
        public int MapWidth = 10;
        public int MapHeight = 10;

        // GUI colors, edited through Unity interface
        public Color GUI_Color_NoBonus;
        public Color GUI_Color_PositiveBonus;
        public Color GUI_Color_NegativeBonus;

        // GUI elements, edited through Unity interface
        public Text GUI_Text_TileName;
        public Text GUI_Text_DefenseBonus;
        public Text GUI_Text_MovementBonus;
        public Text GUI_Text_HealthRegenBonus;
        public Image GUI_Image_DefenseBonus;
        public Image GUI_Image_MovementBonus;
        public Image GUI_Image_HealthRegenBonus;

        void Start()
        {
            DestroyMap();
            GenerateMap(MapSeed, MapWidth, MapHeight);
        }

        // Destroys the tile map gameobject and all tile gameobjects associated with it
        private void DestroyMap()
        {
            GameObject[] tileObjects = GameObject.FindGameObjectsWithTag("Tile");
            GameObject[] tileMapObjects = GameObject.FindGameObjectsWithTag("TileMap");

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
            tileMapObject.tag = "TileMap";

            // Create tile map and array of tiles
            TileMap tileMap = tileMapObject.AddComponent<TileMap>();
            Tile[,] tiles = new Tile[mapWidth, mapHeight];

            // Initialize the noise generator
            NoiseGenerator noiseGenerator = new NoiseGenerator(mapWidth, mapHeight);

            // Initialize tile prefab selector
            TilePrefabSelector tilePrefabSelector = new TilePrefabSelector();

            // Loop through each position in the tile map and create tile object
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    // Generate perlin noise to determine which tile to use
                    float noiseValue = noiseGenerator.GetNoise(x, y);

                    // Get tile prefab from the tile selector with the given noise value
                    GameObject tilePrefab = tilePrefabSelector.GetTilePrefab(noiseValue);
                    // Initialize tile object from the prefab
                    GameObject tileObject = GameObject.Instantiate(tilePrefab) as GameObject;
                    // Get the tile data from the prefab
                    Tile tile = tileObject.GetComponent<Tile>();

                    // Set tile object name, parent, and position within the tile map
                    tileObject.name = tilePrefab.name + " (" + x + ", " + y + ")";
                    tileObject.transform.parent = tileMapObject.transform;
                    tileObject.transform.position = new Vector3(x, y, 0);

                    // Set GUI colors
                    tile.GUI_Color_NoBonus = GUI_Color_NoBonus;
                    tile.GUI_Color_PositiveBonus = GUI_Color_PositiveBonus;
                    tile.GUI_Color_NegativeBonus = GUI_Color_NegativeBonus;

                    // Link GUI elements to tile
                    tile.GUI_Text_TileName = GUI_Text_TileName;
                    tile.GUI_Text_DefenseBonus = GUI_Text_DefenseBonus;
                    tile.GUI_Text_MovementBonus = GUI_Text_MovementBonus;
                    tile.GUI_Text_HealthRegenBonus = GUI_Text_HealthRegenBonus;
                    tile.GUI_Image_DefenseBonus = GUI_Image_DefenseBonus;
                    tile.GUI_Image_MovementBonus = GUI_Image_MovementBonus;
                    tile.GUI_Image_HealthRegenBonus = GUI_Image_HealthRegenBonus;

                    // Update tile coordinates
                    tile.X = x;
                    tile.Y = y;

                    // Update the tiles array
                    tiles[x, y] = tile;
                }
            }

            // Set tile map data
            tileMap.SetMapData(mapSeed, mapWidth, mapHeight, tiles);

            // Call event since map generation is complete
            if (OnGenerationComplete != null)
                OnGenerationComplete(mapWidth, mapHeight);
        }
    }
}