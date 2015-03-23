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
        // Event handlers for in-game entity interactions
        public delegate void MouseOverHandler(GameObject gameObject, int x, int y);
        public static event MouseOverHandler OnMouseOverUnit;
        public static event MouseOverHandler OnMouseOverTile;

        // Current location of the mouse cursor in world space
        public Vector2 Coordinates { get; private set; }

        // Current coordinates in world space, stored as ints
        public int XCoordinateInt { get; private set; }
        public int YCoordinateInt { get; private set; }

        // Layer masks for units and tiles, edited through Unity interface
        public LayerMask UnitCollisionLayer = -1;
        public LayerMask TileCollisionLayer = -1;

        // Raycast to check for mouse interactions with in-game entities
        private RaycastHit2D raycastHit;

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
            CheckRaycastForHits();
        }
         
        // Updates the mouse cursor's coordinates, using generic calculations
        private void UpdateCoordinates(Vector2 mousePosition)
        {
            Coordinates = Algorithms.ConvertPositionToWorldCoordinates(mousePosition);
            transform.position = Coordinates;

            XCoordinateInt = (int)Coordinates.x;
            YCoordinateInt = (int)Coordinates.y;
        }

        // Check the raycast to see if it collided with any units or tiles
        private void CheckRaycastForHits()
        {
            // Check for tile collisions
            raycastHit = Physics2D.Raycast(transform.position, Vector2.up, 0.0f, TileCollisionLayer);
            // If tile was moused over, send current coordinates
            if (OnMouseOverTile != null)
            {
                if (raycastHit.collider != null)
                    OnMouseOverTile(raycastHit.collider.gameObject, XCoordinateInt, YCoordinateInt);
                else
                    OnMouseOverTile(null, XCoordinateInt, YCoordinateInt);
            }

            // Check for unit collisions
            raycastHit = Physics2D.Raycast(transform.position, Vector2.up, 0.0f, UnitCollisionLayer);
            // If unit was moused over, send current coordinates
            if (OnMouseOverUnit != null)
            {
                if (raycastHit.collider != null)
                    OnMouseOverUnit(raycastHit.collider.gameObject, XCoordinateInt, YCoordinateInt);
                else
                    OnMouseOverUnit(null, XCoordinateInt, YCoordinateInt);
            }
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