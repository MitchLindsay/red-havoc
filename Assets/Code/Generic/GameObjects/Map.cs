using UnityEngine;

namespace Assets.Code.Generic.GameObjects
{
    public abstract class Map<T> : MonoBehaviour
    {
        public int Seed { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public T[,] Data { get; private set; }

        public virtual void InitializeMap(int width, int height, T[,] data)
        {
            this.Width = width;
            this.Height = height;
            this.Data = data;
        }

        public virtual void InitializeMap(int seed, int width, int height, T[,] data)
        {
            this.Seed = seed;
            this.Width = width;
            this.Height = height;
            this.Data = data;
        }

        public T[] GetDataNeighbors(T data)
        {
            Vector2 position = GetPositionByData(data);

            int x = (int)position.x;
            int y = (int)position.y;

            if (!IsPositionWithinBounds(x, y))
                return null;

            T[] neighbors = new T[4];
            neighbors[0] = GetDataByPosition(x, y + 1); // Up
            neighbors[1] = GetDataByPosition(x + 1, y); // Right
            neighbors[2] = GetDataByPosition(x, y - 1); // Down
            neighbors[3] = GetDataByPosition(x - 1, y); // Left

            return neighbors;
        }

        public T GetDataByPosition(int x, int y)
        {
            if (IsPositionWithinBounds(x, y))
                return Data[x, y];

            return default(T);
        }

        public Vector2 GetPositionByData(T data)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (Data[x, y].Equals(data))
                        return new Vector2(x, y);
                }
            }

            return new Vector2(-1, -1);
        }

        private bool IsPositionWithinBounds(int x, int y)
        {
            if (x <= Width && x >= 0 && y <= Height && y >= 0)
                return true;

            return false;
        }
    }
}