using Assets.Code.Actors;
using Assets.Code.Graphs;
using UnityEngine;

namespace Assets.Code.Factories
{
    public class PathfinderFactory : GraphFactory
    {
        public GameObject NodePrefab;
        public Color32 ColorTraversable = new Color32(61, 118, 229, 150);
        public Color32 ColorOutOfReach = new Color32(229, 61, 61, 150);
        public Color32 ColorNeutral = new Color32(255, 255, 255, 0);
        private GameObject pathfinderObject;
        private Pathfinder pathfinder;
        private PathfindingNode[,] pathfindingNodes;
        private GameObject pathfinderNodeObject;
        private PathfindingNode pathfindingNode;

        void Start()
        {
            Destroy();
            Generate();
        }

        public override void Generate()
        {
            pathfinderObject = new GameObject(GraphName);
            pathfinderObject.transform.parent = GraphContainer;
            pathfinderObject.transform.position = GraphPosition;
            pathfinderObject.tag = GraphTag;

            pathfinder = pathfinderObject.AddComponent<Pathfinder>();
            pathfinder.ColorTraversable = ColorTraversable;
            pathfinder.ColorOutOfReach = ColorOutOfReach;
            pathfinder.ColorNeutral = ColorNeutral;

            pathfindingNodes = new PathfindingNode[GraphWidth, GraphHeight];

            for (int y = 0; y < GraphHeight; y++)
            {
                for (int x = 0; x < GraphWidth; x++)
                {
                    pathfinderNodeObject = GameObject.Instantiate(NodePrefab) as GameObject;
                    pathfindingNode = pathfinderNodeObject.GetComponent<PathfindingNode>();
                    pathfindingNode.SetColor(pathfinder.ColorNeutral);

                    pathfinderNodeObject.name = "(" + x + ", " + y + ") " + NodePrefab.name;
                    pathfinderNodeObject.transform.parent = pathfinderObject.transform;
                    pathfinderNodeObject.transform.position = new Vector3(x, y, 0);

                    pathfindingNodes[x, y] = pathfindingNode;
                }
            }

            pathfinder.SetNodes(pathfindingNodes);
        }
    }
}