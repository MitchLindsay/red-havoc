using Assets.Code.Controllers.CameraControllers;
using Assets.Code.Generic;
using Assets.Code.TileMaps.Generators;
using UnityEngine;
using UnityEngine.UI;
using Vectrosity;

namespace Assets.Code.GUI.WorldSpace
{
    // MouseCursor.cs
    [RequireComponent (typeof(SpriteRenderer))]
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

        // Cursor color, set in Unity interface
        public Color CursorColor = Color.white;
        // Cursor width, set in Unity interface
        public float SelectionBoxWidth = 4.0f;

        void Start()
        {
            // Cursor.visible = false;
            CreateCursorSelection();
        }

        void Update()
        {
            UpdateCoordinates(Input.mousePosition);
            DrawCursor();
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
                {
                    OnMouseOverUnit(raycastHit.collider.gameObject, XCoordinateInt, YCoordinateInt);
                    SetColor(raycastHit.collider.gameObject.GetComponent<SpriteRenderer>().color);
                }
                else
                {
                    OnMouseOverUnit(null, XCoordinateInt, YCoordinateInt);
                    SetColor(Color.white);
                }
            }
        }

        // Creates the curstor selection box
        private void CreateCursorSelection()
        {
            // Initailize the vector line
            cursorSelectionLine = new VectorLine("Cursor Selection", new Vector3[5], null, SelectionBoxWidth, LineType.Continuous, Joins.Weld);

            // Set the sorting order (underneath units, above tiles, above grid)
            VectorLine.canvas3D.sortingLayerName = "Tile";
            VectorLine.canvas3D.sortingOrder = 2;
        }

        // Updates the position of the cursor
        private void DrawCursor()
        {
            // Create a rectangle for the selection box
            cursorSelectionLine.MakeRect(new Rect((float)XCoordinateInt, (float)YCoordinateInt, 1.0f, 1.0f));
            // Draw the vector line
            gameObject.GetComponent<SpriteRenderer>().color = CursorColor;
            cursorSelectionLine.color = CursorColor;
            cursorSelectionLine.Draw3D();
        }

        // Change the color of the cursor
        private void SetColor(Color color)
        {
            CursorColor = color;
        }
    }
}