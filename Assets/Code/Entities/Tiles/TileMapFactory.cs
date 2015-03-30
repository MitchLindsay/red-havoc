using Assets.Code.Controllers.States;
using Assets.Code.Entities.Abstract;
using UnityEngine;

namespace Assets.Code.Entities.Tiles
{
    public class TileMapFactory : GraphFactory<TileMap>
    {
        public delegate void GenerateHandler(int width, int height);
        public static event GenerateHandler OnGenerateComplete;

        private GameObject tileMapObj;
        private TileMap tileMap;
        private Tile[,] tiles;
        private GameObject[] tilePrefabs;
        private GameObject tilePrefab;
        private GameObject tileObj;
        private Tile tile;

        void Awake()
        {
            SetPrefabs();
            Destroy();
            Generate();
        }

        public override void Generate()
        {
            tileMapObj = new GameObject(GraphName);
            tileMapObj.transform.parent = GraphContainer;
            tileMapObj.transform.position = GraphPosition;
            tileMapObj.tag = GraphTag;

            tileMap = tileMapObj.AddComponent<TileMap>();
            tiles = new Tile[GraphWidth, GraphHeight];

            for (int y = 0; y < GraphHeight; y++)
            {
                for (int x = 0; x < GraphWidth; x++)
                {
                    tilePrefab = GetPrefab();
                    tileObj = GameObject.Instantiate(tilePrefab) as GameObject;
                    tile = tileObj.GetComponent<Tile>();

                    tileObj.name = "(" + x + ", " + y + ") " + tilePrefab.name;
                    tileObj.transform.parent = tileMapObj.transform;
                    tileObj.transform.position = new Vector3(x, y, 0);

                    tiles[x, y] = tile;
                }
            }

            tileMap.Initialize(tiles);
            if (OnGenerateComplete != null)
                OnGenerateComplete(tileMap.Width(), tileMap.Height());
        }

        private void SetPrefabs()
        {
            tilePrefabs = new GameObject[3];

            tilePrefabs[0] = Resources.Load("Tiles/Tile, Plains", typeof(GameObject)) as GameObject;
            tilePrefabs[1] = Resources.Load("Tiles/Tile, Rocks", typeof(GameObject)) as GameObject;
            tilePrefabs[2] = Resources.Load("Tiles/Tile, Mountains", typeof(GameObject)) as GameObject;
        }

        private GameObject GetPrefab()
        {
            if (tilePrefabs != null)
            {
                int random = Random.Range(0, 3);
                return tilePrefabs[random];
            }

            return null;
        }
    }
}