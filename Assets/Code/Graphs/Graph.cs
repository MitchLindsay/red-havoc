using UnityEngine;

namespace Assets.Code.Graphs
{
    public abstract class Graph<T> : MonoBehaviour
    {
        public T[,] Nodes { get; protected set; }

        public void SetNodes(T[,] nodes)
        {
            this.Nodes = nodes;
        }

        public int Width
        {
            get 
            {
                if (Nodes != null)
                    return Nodes.GetLength(0);

                return 0;
            }
        }

        public int Height
        {
            get
            {
                if (Nodes != null)
                    return Nodes.GetLength(1);

                return 0;
            }
        }

        public T[] Neighbors(T node)
        {
            if (node == null)
                return null;

            Vector2 position = GetPositionByNode(node);

            int x = (int)position.x;
            int y = (int)position.y;

            T[] neighbors = new T[4];

            neighbors[0] = GetNodeByPosition(x, y + 1);
            neighbors[1] = GetNodeByPosition(x + 1, y);
            neighbors[2] = GetNodeByPosition(x, y - 1);
            neighbors[3] = GetNodeByPosition(x - 1, y);

            return neighbors;
        }

        public T GetNodeByPosition(int x, int y)
        {
            if (IsPositionWithinGraph(x, y))
                return Nodes[x, y];

            return default(T);
        }

        public T GetNodeByPosition(Vector2 position)
        {
            int x = (int)position.x;
            int y = (int)position.y;

            if (IsPositionWithinGraph(x, y))
                return Nodes[x, y];

            return default(T);
        }

        public Vector2 GetPositionByNode(T node)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Nodes[x, y].Equals(node))
                        return new Vector2(x, y);
                }
            }

            return default(Vector2);
        }

        public bool IsNodeWithinGraph(T node)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Nodes[x, y].Equals(node))
                        return true;
                }
            }

            return false;
        }

        public bool IsPositionWithinGraph(int x, int y)
        {
            if (x < Width && x >= 0 && y < Height && y >= 0)
                return true;

            return false;
        }
    }
}