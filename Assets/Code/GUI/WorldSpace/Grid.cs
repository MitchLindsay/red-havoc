using Assets.Code.Generic;
using Assets.Code.TileMaps.Generators;
using UnityEngine;
using Vectrosity;

namespace Assets.Code.GUI.WorldSpace
{
    public class Grid : MonoBehaviour
    {
        public float Alpha = 1.0f;
        private VectorLine gridLine;

        void OnEnable()
        {
            TileMapGenerator.OnGenerationComplete += CreateGrid;
        }

        void OnDestroy()
        {
            TileMapGenerator.OnGenerationComplete -= CreateGrid;
        }

        private void CreateGrid(int width, int height)
        {
            int numPoints = width * height;

            if (Algorithms.IsNumberOdd(numPoints))
                numPoints += 1;

            gridLine = new VectorLine("Grid", new Vector3[numPoints], null, 1.0f);
            
            int index = 0;
            for (int x = 0; x < width + 1; x++)
            {
                gridLine.points3[index++] = new Vector3(x, 0.0f, 0.0f);
                gridLine.points3[index++] = new Vector3(x, height, 0.0f);
            }
            for (int y = 0; y < height + 1; y++)
            {
                gridLine.points3[index++] = new Vector3(0.0f, y, 0.0f);
                gridLine.points3[index++] = new Vector3(width, y, 0.0f);
            }

            VectorLine.canvas3D.pixelPerfect = true;
            VectorLine.canvas3D.sortingLayerName = "Tiles";
            VectorLine.canvas3D.sortingOrder = 1;

            gridLine.SetColor(new Color(0.0f, 0.0f, 0.0f, Alpha));
            gridLine.Draw3DAuto();
        }
    }
}