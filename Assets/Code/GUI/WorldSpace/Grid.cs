using Assets.Code.Generic;
using Assets.Code.TileMaps.Generators;
using UnityEngine;
using Vectrosity;

namespace Assets.Code.GUI.WorldSpace
{
    // Grid.cs - Uses Vectrosity to draw a grid over the tile map
    public class Grid : MonoBehaviour
    {
        // Flag to check if the grid is visible
        public bool IsVisible { get; private set; }
        // Flag to check if the grid is drawn to the screen
        public bool IsEnabled { get; private set; }

        // Dimensions of the grid
        public int GridWidth { get; private set; }
        public int GridHeight { get; private set; }

        // Vector line for the grid
        public VectorLine GridLine { get; private set; }
        // Array containing coordinates for the vector line
        public Vector3[] GridLinePoints { get; private set; }

        void OnEnable()
        {
            TileMapGenerator.OnGenerationComplete += SetGrid;
        }

        void OnDestroy()
        {
            TileMapGenerator.OnGenerationComplete -= SetGrid;
        }

        void Start()
        {
            SetVisible(true);
            SetEnabled(true);
        }

        void Update()
        {
            if (IsEnabled && GridLine != null)
            {
                VectorLine.canvas.sortingOrder = -1;
                VectorLine.SetCanvasCamera(Camera.main);

                DrawGrid();
            }
        }

        private void SetVisible(bool isVisible)
        {
            IsVisible = isVisible;
            GridLine.active = isVisible;
        }

        private void SetEnabled(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }

        private void SetGrid(int width, int height)
        {
            GridWidth = width;
            GridHeight = height;

            int lineSize = GridWidth * GridHeight;

            if (Algorithms.IsNumberEven(lineSize))
                GridLinePoints = new Vector3[lineSize];
            else
                GridLinePoints = new Vector3[lineSize + 1];

            GridLine = new VectorLine("Grid", GridLinePoints, null, 1.0f);
            GridLine.rectTransform.anchoredPosition = new Vector2(0.5f, 0.5f);
        }

        private void DrawGrid()
        {
            int index = 0;

            for (int x = 0; x < GridWidth; x++)
            {
                GridLinePoints[index++] = new Vector2(x, 0.0f);
                GridLinePoints[index++] = new Vector2(x, GridHeight);
            }

            for (int y = 0; y < GridHeight; y++)
            {
                GridLinePoints[index++] = new Vector2(0.0f, y);
                GridLinePoints[index++] = new Vector2(GridWidth, y);
            }

            GridLine.Draw();
        }
    }
}