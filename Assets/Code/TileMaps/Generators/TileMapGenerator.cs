using Assets.Code.TileMaps.Entities;
using UnityEngine;

namespace Assets.Code.TileMaps.Generators
{
    public class TileMapGenerator : MonoBehaviour
    {
        public delegate void MapGenerationHandler(int width, int height);
        public static event MapGenerationHandler OnGenerationComplete;

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

        public int MapSeed = 1;
        public int MapWidth = 10;
        public int MapHeight = 10;

        void Start()
        {
            DestroyMap();
            GenerateMap(MapSeed, MapWidth, MapHeight);
        }

        private void DestroyMap()
        {
            GameObject[] tileObjects = GameObject.FindGameObjectsWithTag("Tile");
            GameObject[] tileMapObjects = GameObject.FindGameObjectsWithTag("TileMap");

            foreach (GameObject tileObject in tileObjects)
                DestroyImmediate(tileObject);

            foreach (GameObject tileMapObject in tileMapObjects)
                DestroyImmediate(tileMapObject);
        }

        private void GenerateMap(int mapSeed, int mapWidth, int mapHeight)
        {
            GameObject tileMapObject = new GameObject("Tile Map");
            tileMapObject.transform.position = Vector3.zero;
            tileMapObject.tag = "TileMap";

            TileMap tileMap = tileMapObject.AddComponent<TileMap>();
            Tile[,] tiles = new Tile[mapWidth, mapHeight];

            NoiseGenerator noiseGenerator = new NoiseGenerator(mapWidth, mapHeight);

            TilePrefabSelector tilePrefabSelector = new TilePrefabSelector();

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    float noiseValue = noiseGenerator.GetNoise(x, y);

                    GameObject tilePrefab = tilePrefabSelector.GetTilePrefab(noiseValue);
                    GameObject tileObject = GameObject.Instantiate(tilePrefab) as GameObject;
                    Tile tile = tileObject.GetComponent<Tile>();

                    tileObject.name = tilePrefab.name + " (" + x + ", " + y + ")";
                    tileObject.transform.parent = tileMapObject.transform;
                    tileObject.transform.position = new Vector3(x, y, 0);

                    tiles[x, y] = tile;
                }
            }

            tileMap.SetMapData(mapSeed, mapWidth, mapHeight, tiles);

            if (OnGenerationComplete != null)
                OnGenerationComplete(mapWidth, mapHeight);
        }
    }
}