using Assets.Code.Actors;
using Assets.Code.Generic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Graphs
{
    public class Pathfinder : Graph<PathfindingNode>
    {
        public delegate void PathGenerateHandler(List<Vector2> path);
        public static event PathGenerateHandler OnPathGenerateComplete;

        public Color32 ColorTraversable = new Color32(61, 118, 229, 150);
        public Color32 ColorOutOfReach = new Color32(229, 61, 61, 150);
        public Color32 ColorNeutral = new Color32(255, 255, 255, 0);

        private bool pathfindingEnabled = false;
        private PathfindingNode startNode = null;
        private PathfindingNode goalNode = null;
        private HashSet<PathfindingNode> area = null;

        private HashSet<PathfindingNode> explored;
        private Dictionary<PathfindingNode, PathfindingNode> cameFrom;
        private Dictionary<PathfindingNode, int> costSoFar;
        private PathfindingNode[] neighbors;
        private PathfindingNode current;
        private int newCost;
        private int priority;

        void OnEnable()
        {
            Actors.Cursor.OnMouseOverNode += ShowPath;
        }

        void OnDestroy()
        {
            Actors.Cursor.OnMouseOverNode -= ShowPath;
        }

        private void EnablePathfinding()
        {
            pathfindingEnabled = true;
        }

        private void DisablePathfinding()
        {
            pathfindingEnabled = false;
        }

        public void ShowArea(Unit unit, int distance)
        {
            if (unit != null)
            {
                startNode = GetNodeByPosition(unit.transform.position);

                HashSet<PathfindingNode> tempArea = BreadthFirstSearch(startNode, distance);
                PathfindingNode[] neighbors;

                if (unit.IsActive)
                {
                    foreach (PathfindingNode node in tempArea)
                    {
                        node.SetColor(ColorTraversable);
                        neighbors = Neighbors(node);
                        foreach (PathfindingNode neighbor in neighbors)
                        {
                            if (neighbor != null && !tempArea.Contains(neighbor))
                                neighbor.SetColor(ColorOutOfReach);
                        }
                    }

                    area = tempArea;
                    EnablePathfinding();
                }
                else
                {
                    DisablePathfinding();

                    foreach (PathfindingNode node in tempArea)
                        node.SetColor(ColorOutOfReach);

                    startNode = null;
                    area = null;
                }
            }
        }

        public void HideArea()
        {
            DisablePathfinding();

            foreach (PathfindingNode node in Nodes)
                node.SetColor(ColorNeutral);
        }

        private void ShowPath(GameObject goalObject)
        {
            if (pathfindingEnabled && goalObject != null)
            {
                goalNode = goalObject.GetComponent<PathfindingNode>();

                List<PathfindingNode> path = AStarSearch(startNode, goalNode, area);
                List<Vector2> pathPositions = new List<Vector2>();

                if (path != null)
                {
                    for (int i = 0; i < path.Count; i++)
                        pathPositions.Add(path[i].transform.position);
                }

                if (OnPathGenerateComplete != null)
                    OnPathGenerateComplete(pathPositions);
            }
        }

        private HashSet<PathfindingNode> BreadthFirstSearch(PathfindingNode start, int distance)
        {
            if (start == null || distance <= 0)
                return null;

            Queue<PathfindingNode> reachable = new Queue<PathfindingNode>();
            reachable.Enqueue(start);

            explored = new HashSet<PathfindingNode>();
            explored.Add(start);

            costSoFar = new Dictionary<PathfindingNode, int>();
            costSoFar.Add(start, 0);

            while (reachable.Count > 0)
            {
                current = reachable.Dequeue();
                explored.Add(current);

                neighbors = Neighbors(current);
                foreach (PathfindingNode neighbor in neighbors)
                {
                    if (neighbor == null || explored.Contains(neighbor) || !neighbor.Traversable)
                        continue;

                    newCost = costSoFar[current] + neighbor.MoveCost;
                    costSoFar[neighbor] = newCost;

                    if (newCost <= distance)
                    {
                        explored.Add(neighbor);
                        reachable.Enqueue(neighbor);
                    }
                }
            }

            return explored;
        }

        private List<PathfindingNode> AStarSearch(PathfindingNode start, PathfindingNode goal, HashSet<PathfindingNode> area)
        {
            if (start == null || goal == null || !goal.Traversable || start == goal || area == null || !area.Contains(goal))
                return null;

            PriorityQueue<PathfindingNode> reachable = new PriorityQueue<PathfindingNode>();
            reachable.Enqueue(start, 0);

            cameFrom = new Dictionary<PathfindingNode, PathfindingNode>();
            cameFrom.Add(start, null);

            costSoFar = new Dictionary<PathfindingNode, int>();
            costSoFar.Add(start, 0);

            while (!reachable.Empty)
            {
                current = reachable.Dequeue();

                if (current == goal)
                    return GeneratePath(cameFrom, goal);

                neighbors = Neighbors(current);
                foreach (PathfindingNode neighbor in neighbors)
                {
                    if (!area.Contains(neighbor))
                        continue;

                    newCost = costSoFar[current] + neighbor.MoveCost;

                    if (!costSoFar.ContainsKey(neighbor) || (newCost < costSoFar[neighbor]))
                    {
                        priority = newCost + Heuristic(neighbor, goal);
                        reachable.Enqueue(neighbor, priority);

                        if (!costSoFar.ContainsKey(neighbor))
                            costSoFar.Add(neighbor, newCost);

                        if (!cameFrom.ContainsKey(neighbor))
                            cameFrom.Add(neighbor, current);
                    }
                }
            }

            return null;
        }

        private List<PathfindingNode> GeneratePath(Dictionary<PathfindingNode, PathfindingNode> cameFrom, PathfindingNode goal)
        {
            List<PathfindingNode> path = new List<PathfindingNode>();
            PathfindingNode temp = goal;

            while (temp != null)
            {
                path.Add(temp);
                temp = cameFrom[temp];
            }

            path.Reverse();
            return path;
        }

        private int Heuristic(PathfindingNode nodeA, PathfindingNode nodeB)
        {
            return Algorithms.GetManhattanDistance(nodeA.transform.position, nodeB.transform.position);
        }
    }
}