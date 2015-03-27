using Assets.Code.Entities.TileMaps;
using Assets.Code.Generic.Factories;
using UnityEngine;

namespace Assets.Code.Entities.Pathfinding
{
    public class PathfindingGenerator : MapGenerator<Pathfinder>
    {
        public delegate void MapGenerationHandler(int width, int height);
        public static event MapGenerationHandler OnGenerationComplete;

        void OnEnable()
        {
            TileMapGenerator.OnGenerationComplete += CopyTileMapData;
        }

        void OnDestroy()
        {
            TileMapGenerator.OnGenerationComplete -= CopyTileMapData;
        }

        private void CopyTileMapData(TileMap tileMap)
        {
            this.Width = tileMap.Width;
            this.Height = tileMap.Height;

            DestroyMap();
            GenerateMap(tileMap.Data);
        }

        public void GenerateMap(Tile[,] tileMapData)
        {
            GameObject mapObject = new GameObject("Pathfinder");
            mapObject.transform.parent = MapContainer.transform;
            mapObject.transform.position = MapLocation;
            mapObject.tag = "Pathfinder";

            Pathfinder map = mapObject.AddComponent<Pathfinder>();
            PathfindingNode[,] data = new PathfindingNode[Width, Height];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Tile tile = tileMapData[x, y];
                    GameObject tileObject = tile.gameObject;

                    GameObject nodePrefab = Resources.Load("Pathfinding/PathfindingNode", typeof(GameObject)) as GameObject;
                    GameObject nodeObject = GameObject.Instantiate(nodePrefab) as GameObject;
                    PathfindingNode node = nodeObject.GetComponent<PathfindingNode>();

                    node.InitializeNode(Mathf.Abs(tile.MovementBonus));
                    nodeObject.name = nodePrefab.name + " (" + x + ", " + y + ")";
                    nodeObject.transform.parent = mapObject.transform;
                    nodeObject.transform.position = new Vector3(x, y, 0);

                    data[x, y] = node;
                }
            }

            map.InitializeMap(Width, Height, data);

            if (OnGenerationComplete != null)
                OnGenerationComplete(Width, Height);
        }
    }
}