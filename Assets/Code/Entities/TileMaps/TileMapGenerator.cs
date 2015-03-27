using Assets.Code.Generic.Factories;
using UnityEngine;

namespace Assets.Code.Entities.TileMaps
{
    public class TileMapGenerator : MapGenerator<TileMap>
    {
        public delegate void MapGenerationHandler(TileMap tileMap);
        public static event MapGenerationHandler OnGenerationComplete;

        public int Seed = 1;

        void Start()
        {
            DestroyMap();
            GenerateMap();
        }

        public void GenerateMap()
        {
            GameObject mapObject = new GameObject("Tile Map");
            mapObject.transform.parent = MapContainer.transform;
            mapObject.transform.position = MapLocation;
            mapObject.tag = "TileMap";

            TileMap map = mapObject.AddComponent<TileMap>();
            Tile[,] data = new Tile[Width, Height];

            NoiseGenerator noiseGenerator = new NoiseGenerator(Width, Height);
            TilePrefabSelector tilePrefabSelector = new TilePrefabSelector();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    float noiseValue = noiseGenerator.GetNoise(x, y);

                    GameObject tilePrefab = tilePrefabSelector.GetTilePrefab(noiseValue);
                    GameObject tileObject = GameObject.Instantiate(tilePrefab) as GameObject;
                    Tile tile = tileObject.GetComponent<Tile>();

                    tileObject.name = tilePrefab.name + " (" + x + ", " + y + ")";
                    tileObject.transform.parent = mapObject.transform;
                    tileObject.transform.position = new Vector3(x, y, 0);

                    data[x, y] = tile;
                }
            }

            map.InitializeMap(Seed, Width, Height, data);

            if (OnGenerationComplete != null)
                OnGenerationComplete(map);
        }
    }
}