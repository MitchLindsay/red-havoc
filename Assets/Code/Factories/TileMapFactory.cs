using Assets.Code.Actors;
using Assets.Code.Graphs;
using UnityEngine;

namespace Assets.Code.Factories
{
    public class TileMapFactory : GraphFactory
    {
        public delegate void TileMapGeneration(int width, int height);
        public static event TileMapGeneration OnGenerationComplete;

        private GameObject tileMapObject;
        private TileMap tileMap;
        private Tile[,] tiles;
        private GameObject[] tilePrefabs;
        private GameObject tilePrefab;
        private GameObject tileObject;
        private Tile tile;

        void Start()
        {
            SetTilePrefabs();
            Destroy();
            Generate();
        }

        public override void Generate()
        {
            tileMapObject = new GameObject(GraphName);
            tileMapObject.transform.parent = GraphContainer;
            tileMapObject.transform.position = GraphPosition;
            tileMapObject.tag = GraphTag;

            tileMap = tileMapObject.AddComponent<TileMap>();
            tiles = new Tile[GraphWidth, GraphHeight];

            for (int y = 0; y < GraphHeight; y++)
            {
                for (int x = 0; x < GraphWidth; x++)
                {
                    tilePrefab = GetTilePrefab();
                    tileObject = GameObject.Instantiate(tilePrefab) as GameObject;
                    tile = tileObject.GetComponent<Tile>();

                    tileObject.name = "(" + x + ", " + y + ") " + tilePrefab.name;
                    tileObject.transform.parent = tileMapObject.transform;
                    tileObject.transform.position = new Vector3(x, y, 0);

                    tiles[x, y] = tile;
                }
            }

            tileMap.SetNodes(tiles);

            if (OnGenerationComplete != null)
                OnGenerationComplete(GraphWidth, GraphHeight);
        }

        private void SetTilePrefabs()
        {
            tilePrefabs = new GameObject[3];

            tilePrefabs[0] = Resources.Load("Tiles/Tile, Plains", typeof(GameObject)) as GameObject;
            tilePrefabs[1] = Resources.Load("Tiles/Tile, Rocks", typeof(GameObject)) as GameObject;
            tilePrefabs[2] = Resources.Load("Tiles/Tile, Mountains", typeof(GameObject)) as GameObject;
        }

        private GameObject GetTilePrefab()
        {
            if (tilePrefabs != null && tilePrefabs.Length > 0)
            {
                int random = Random.Range(0, tilePrefabs.Length);
                return tilePrefabs[random];
            }

            return null;
        }
    }
}