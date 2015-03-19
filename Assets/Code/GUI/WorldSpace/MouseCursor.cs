using Assets.Code.Controllers.CameraControllers;
using Assets.Code.Generic;
using Assets.Code.TileMaps.Generators;
using UnityEngine;
using UnityEngine.UI;
using Vectrosity;

namespace Assets.Code.GUI.WorldSpace
{
    // MouseCursor.cs
    public class MouseCursor : MonoBehaviour
    {
        // Current location of the mouse cursor in world space
        public Vector2 Coordinates { get; private set; }

        // Current coordinates in world space, stored as ints
        public int XCoordinateInt { get; private set; }
        public int YCoordinateInt { get; private set; }

        // Vector line for the cursor selection box
        private VectorLine cursorSelectionLine;

        void Start()
        {
            CreateCursorSelection();
        }

        void Update()
        {
            UpdateCoordinates(Input.mousePosition);
            DrawCursorSelection();
        }
         
        // Updates the mouse cursor's coordinates, using generic calculations
        private void UpdateCoordinates(Vector2 mousePosition)
        {
            Coordinates = Algorithms.ConvertPositionToWorldCoordinates(mousePosition);

            XCoordinateInt = (int)Coordinates.x;
            YCoordinateInt = (int)Coordinates.y;
        }

        // Creates the curstor selection box
        private void CreateCursorSelection()
        {
            // Initailize the vector line
            cursorSelectionLine = new VectorLine("Cursor Selection", new Vector3[5], null, 1.0f, LineType.Continuous, Joins.Weld);

            // Set the sorting order (underneath units, above tiles, above grid)
            VectorLine.canvas3D.sortingLayerName = "Tile";
            VectorLine.canvas3D.sortingOrder = 2;
        }

        // Updates the position of the cursor selection box
        private void DrawCursorSelection()
        {
            // Create a rectangle for the selection box
            cursorSelectionLine.MakeRect(new Rect((float)XCoordinateInt, (float)YCoordinateInt, 1.0f, 1.0f));
            // Draw the vector line
            cursorSelectionLine.Draw3D();
        }
    }
}