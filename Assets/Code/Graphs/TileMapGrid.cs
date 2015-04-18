using Assets.Code.Factories;
using Assets.Code.Generic;
using UnityEngine;
using Vectrosity;

namespace Assets.Code.Graphs
{
    public class TileMapGrid : MonoBehaviour
    {
        public float GridOpacity = 0.5f;
        public float GridWidth = 1.0f;
        private VectorLine line;

        void OnEnable()
        {
            TileMapFactory.OnGenerationComplete += Generate;
        }

        void OnDestroy()
        {
            TileMapFactory.OnGenerationComplete -= Generate;
        }

        private void Generate(int width, int height)
        {
            int numPoints = width * height;

            if (Algorithms.IsNumberOdd(numPoints))
                numPoints += 1;

            line = new VectorLine("Tile Map Grid", new Vector3[numPoints], null, GridWidth);

            int index = 0;
            for (int x = 0; x < width + 1; x++)
            {
                line.points3[index++] = new Vector3(x, 0.0f, 0.0f);
                line.points3[index++] = new Vector3(x, height, 0.0f);
            }
            for (int y = 0; y < height + 1; y++)
            {
                line.points3[index++] = new Vector3(0.0f, y, 0.0f);
                line.points3[index++] = new Vector3(width, y, 0.0f);
            }

            VectorLine.canvas3D.pixelPerfect = true;
            VectorLine.canvas3D.sortingLayerName = "Tiles";
            VectorLine.canvas3D.sortingOrder = 1;

            line.SetColor(new Color(0.0f, 0.0f, 0.0f, GridOpacity));
            line.Draw3DAuto();
        }
    }
}