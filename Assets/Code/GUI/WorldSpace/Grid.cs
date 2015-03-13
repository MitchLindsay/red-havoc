using Assets.Code.Generic;
using Assets.Code.TileMaps.Generators;
using UnityEngine;
using Vectrosity;

namespace Assets.Code.GUI.WorldSpace
{
    // Grid.cs - Uses Vectrosity to draw a grid over the tile map
    public class Grid : MonoBehaviour
    {
        // Vector line for the grid
        private VectorLine gridLine;

        // Listen for events when object is created
        void OnEnable()
        {
            TileMapGenerator.OnGenerationComplete += CreateGrid;
        }

        // Stop listening for events if object is destroyed
        void OnDestroy()
        {
            TileMapGenerator.OnGenerationComplete -= CreateGrid;
        }

        // Creates and draws the grid
        private void CreateGrid(int width, int height)
        {
            // Calculate number of grid points
            int numPoints = width * height;
            // Create the grid line
            gridLine = new VectorLine("Grid", new Vector3[numPoints], null, 1.0f);

            // Loop through each point of the grid
            int index = 0;
            for (int x = 0; x < width; x++)
            {
                gridLine.points3[index++] = new Vector3(x, 0.0f, 0.0f);
                gridLine.points3[index++] = new Vector3(x, height, 0.0f);
            }
            for (int y = 0; y < height; y++)
            {
                gridLine.points3[index++] = new Vector3(0.0f, y, 0.0f);
                gridLine.points3[index++] = new Vector3(width, y, 0.0f);
            }

            // Set canvas to pixel perfect
            VectorLine.canvas3D.pixelPerfect = true;

            // Change canvas sorting order (underneath units, but above tiles)
            VectorLine.canvas3D.sortingLayerName = "Tiles";
            VectorLine.canvas3D.sortingOrder = 1;

            // Set grid color to black
            gridLine.SetColor(Color.black);

            // Draw grid
            gridLine.Draw3D();
        }
    }
}