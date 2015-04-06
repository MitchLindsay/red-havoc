using Assets.Code.Controllers.States;
using Assets.Code.Entities.Abstract;
using Assets.Code.GUI.World;
using Assets.Code.Libraries;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Entities.Pathfinding
{
    public class Pathfinder : Graph<Node> 
    {
        public delegate void PathGenerationHandler(List<Vector2> path);
        public static event PathGenerationHandler OnPathGenerationComplete;

        private Vector2 startPosition;
        private Vector2 goalPosition;
        private Dictionary<Node, Node> cameFrom;
        private Dictionary<Node, int> costSoFar;

        void OnEnable()
        {
            SelectUnitState.OnUnitSelect += SetStartPosition;
            MouseCursor.OnMouseOverNode += SetGoalPosition;
            MoveUnitState.OnNodeChange += FindPath;
        }

        void OnDestroy()
        {
            SelectUnitState.OnUnitSelect -= SetStartPosition;
            MouseCursor.OnMouseOverNode -= SetGoalPosition;
            MoveUnitState.OnNodeChange -= FindPath;
        }

        private void SetStartPosition(GameObject gameObject)
        {
            if (gameObject != null)
                startPosition = gameObject.transform.position;
            else
                startPosition = new Vector2(-1.0f, -1.0f);
        }

        private void SetGoalPosition(GameObject gameObject)
        {
            if (gameObject != null)
                goalPosition = gameObject.transform.position;
            else
                goalPosition = new Vector2(-1.0f, -1.0f);
        }

        public void FindPath()
        {
            Node start = GetNodeByPosition(startPosition);
            Node goal = GetNodeByPosition(goalPosition);

            if (start == null || goal == null || !goal.CanBeGoal)
                GeneratePath(null);

            cameFrom = new Dictionary<Node, Node>();
            costSoFar = new Dictionary<Node, int>();

            PriorityQueue<int, Node> reachable = new PriorityQueue<int,Node>();
            reachable.Enqueue(0, start);

            HashSet<Node> explored = new HashSet<Node>();
            explored.Add(start);

            cameFrom.Add(start, null);
            costSoFar.Add(start, 0);

            while (!reachable.IsEmpty)
            {
                Node current = reachable.Dequeue();
                explored.Add(current);

                if (current == goal)
                    GeneratePath(goal);

                foreach (Node neighbor in Neighbors(current))
                {
                    if (neighbor == null || explored.Contains(neighbor) || !neighbor.Traversable)
                        continue;

                    int newCost = costSoFar[current] + neighbor.MoveCost;
                    if (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor])
                    {
                        costSoFar.Add(neighbor, newCost);
                        int priority = newCost + Heuristic(neighbor, goal);

                        reachable.Enqueue(priority, neighbor);
                        cameFrom.Add(neighbor, current);
                    }
                }
            }

            GeneratePath(null);
        }

        private void GeneratePath(Node goal)
        {
            List<Vector2> path = null;
            Node temp = goal;

            if (goal != null)
            {
                path = new List<Vector2>();

                while (temp != null)
                {
                    path.Add(temp.transform.position);
                    temp = cameFrom[temp];
                }

                path.Reverse();
            }

            if (OnPathGenerationComplete != null)
                OnPathGenerationComplete(path);
        }

        private int Heuristic(Node nodeA, Node nodeB)
        {
            return Algorithms.GetManhattanDistance(nodeA.transform.position, nodeB.transform.position);
        }
    }
}