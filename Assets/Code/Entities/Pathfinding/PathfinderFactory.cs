using Assets.Code.Entities.Abstract;
using UnityEngine;

namespace Assets.Code.Entities.Pathfinding
{
    public class PathfinderFactory : GraphFactory<Pathfinder>
    {
        public GameObject NodePrefab;

        private GameObject pathfinderObj;
        private Pathfinder pathfinder;
        private Node[,] nodes;
        private GameObject nodeObj;
        private Node node;

        void Start()
        {
            Destroy();
            Generate();
        }

        public override void Generate()
        {
            pathfinderObj = new GameObject(GraphName);
            pathfinderObj.transform.parent = GraphContainer;
            pathfinderObj.transform.position = GraphPosition;
            pathfinderObj.tag = GraphTag;

            pathfinder = pathfinderObj.AddComponent<Pathfinder>();
            nodes = new Node[GraphWidth, GraphHeight];

            for (int y = 0; y < GraphHeight; y++)
            {
                for (int x = 0; x < GraphWidth; x++)
                {
                    nodeObj = GameObject.Instantiate(NodePrefab) as GameObject;
                    node = nodeObj.GetComponent<Node>();

                    nodeObj.name = "(" + x + ", " + y + ") " + NodePrefab.name;
                    nodeObj.transform.parent = pathfinderObj.transform;
                    nodeObj.transform.position = new Vector3(x, y, 0);

                    nodes[x, y] = node;
                }
            }

            pathfinder.Initialize(nodes);
        }
    }
}