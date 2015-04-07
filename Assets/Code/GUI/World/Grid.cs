using Assets.Code.Entities.Tiles;
using Assets.Code.Libraries;
using UnityEngine;
using Vectrosity;

namespace Assets.Code.GUI.World
{
    public class Grid : MonoBehaviour
    {
        public bool GridEnabled = true;
        public float Alpha = 0.5f;
        private VectorLine gridLine;

        void OnEnable()
        {
            TileMapFactory.OnGenerateComplete += Generate;
        }

        void OnDestroy()
        {
            TileMapFactory.OnGenerateComplete -= Generate;
        }

        void Update()
        {
            if (GridEnabled)
                Show();
            else
                Hide();
        }

        private void Generate(int width, int height)
        {
            int numPoints = width * height;

            if (Algorithms.IsNumberOdd(numPoints))
                numPoints += 1;

            gridLine = new VectorLine("TileMap Grid", new Vector3[numPoints], null, 1.0f);

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

        private void Show()
        {
            gridLine.active = true;
        }

        private void Hide()
        {
            gridLine.active = false;
        }
    }
}