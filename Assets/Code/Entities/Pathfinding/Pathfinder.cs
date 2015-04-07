using Assets.Code.Controllers.States;
using Assets.Code.Entities.Abstract;
using Assets.Code.Entities.Units;
using Assets.Code.GUI.World;
using Assets.Code.Libraries;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Entities.Pathfinding
{
    public class Pathfinder : Graph<Node>
    {
        public delegate void PathGenerateHandler(List<Vector2> path);
        public static event PathGenerateHandler OnPathGenerateComplete;

        public Color32 ColorTraversable = new Color32(61, 118, 229, 150);
        public Color32 ColorOutOfReach = new Color32(229, 61, 61, 150);
        public Color32 ColorNeutral = new Color32(255, 255, 255, 0);

        private bool pathfindingEnabled = false;
        private Node startNode = null;
        private Node goalNode = null;
        private HashSet<Node> area = null;

        private HashSet<Node> explored;
        private Dictionary<Node, Node> cameFrom;
        private Dictionary<Node, int> costSoFar;
        private Node[] neighbors;
        private Node current;
        private int newCost;
        private int priority;

        void OnEnable()
        {
            SelectUnitState.OnUnitSelect += ShowArea;
            MoveUnitState.OnStateEntry += EnablePathfinding;
            MoveUnitState.OnStateExit += DisablePathfinding;
            MoveUnitState.OnUnitDeselect += HideArea;
            MouseCursor.OnMouseOverNode += ShowPath;
            SelectUnitCommandState.OnStateEntry += HideArea;
        }

        void OnDestroy()
        {
            SelectUnitState.OnUnitSelect -= ShowArea;
            MoveUnitState.OnStateEntry -= EnablePathfinding;
            MoveUnitState.OnStateExit -= DisablePathfinding;
            MoveUnitState.OnUnitDeselect -= HideArea;
            MouseCursor.OnMouseOverNode -= ShowPath;
            SelectUnitCommandState.OnStateEntry -= HideArea;
        }

        private void EnablePathfinding()
        {
            pathfindingEnabled = true;
        }

        private void DisablePathfinding()
        {
            pathfindingEnabled = false;
        }

        private void ShowArea(GameObject startObject)
        {
            if (startObject != null)
            {
                Unit unit = startObject.GetComponent<Unit>();
                if (unit != null)
                {
                    startNode = GetNodeByPosition(unit.transform.position);
                    int distance = unit.Movement.ModifiedValue;

                    HashSet<Node> tempArea = BreadthFirstSearch(startNode, distance);
                    Node[] neighbors;

                    if (unit.IsActive)
                    {
                        foreach (Node node in tempArea)
                        {
                            node.SetColor(ColorTraversable);
                            neighbors = Neighbors(node);
                            foreach (Node neighbor in neighbors)
                            {
                                if (neighbor != null && !tempArea.Contains(neighbor))
                                    neighbor.SetColor(ColorOutOfReach);
                            }
                        }

                        area = tempArea;
                    }
                    else
                    {
                        foreach (Node node in tempArea)
                            node.SetColor(ColorOutOfReach);

                        startNode = null;
                        area = null;
                    }
                }
            }
        }

        private void HideArea()
        {
            foreach (Node node in Nodes)
                node.SetColor(ColorNeutral);
        }

        private void ShowPath(GameObject goalObject)
        {
            if (pathfindingEnabled && goalObject != null)
            {
                goalNode = goalObject.GetComponent<Node>();

                List<Node> path = AStarSearch(startNode, goalNode, area);
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

        private HashSet<Node> BreadthFirstSearch(Node start, int distance)
        {
            if (start == null || distance <= 0)
                return null;

            Queue<Node> reachable = new Queue<Node>();
            reachable.Enqueue(start);

            explored = new HashSet<Node>();
            explored.Add(start);

            costSoFar = new Dictionary<Node, int>();
            costSoFar.Add(start, 0);

            while (reachable.Count > 0)
            {
                current = reachable.Dequeue();
                explored.Add(current);

                neighbors = Neighbors(current);
                foreach (Node neighbor in neighbors)
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

        private List<Node> AStarSearch(Node start, Node goal, HashSet<Node> area)
        {
            if (start == null || goal == null || !goal.CanBeGoal || start == goal || area == null || !area.Contains(goal))
                return null;

            PriorityQueue<Node> reachable = new PriorityQueue<Node>();
            reachable.Enqueue(start, 0);

            cameFrom = new Dictionary<Node, Node>();
            cameFrom.Add(start, null);

            costSoFar = new Dictionary<Node, int>();
            costSoFar.Add(start, 0);

            while (!reachable.Empty)
            {
                current = reachable.Dequeue();

                if (current == goal)
                    return GeneratePath(cameFrom, goal);

                neighbors = Neighbors(current);
                foreach (Node neighbor in neighbors)
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

        private List<Node> GeneratePath(Dictionary<Node, Node> cameFrom, Node goal)
        {
            List<Node> path = new List<Node>();
            Node temp = goal;

            while (temp != null)
            {
                path.Add(temp);
                temp = cameFrom[temp];
            }

            path.Reverse();
            return path;
        }

        private int Heuristic(Node nodeA, Node nodeB)
        {
            return Algorithms.GetManhattanDistance(nodeA.transform.position, nodeB.transform.position);
        }
    }
}