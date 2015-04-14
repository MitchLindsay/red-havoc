using Assets.Code.Controllers;
using Assets.Code.Factories;
using Assets.Code.Generic;
using UnityEngine;
using Vectrosity;

namespace Assets.Code.Actors
{
    public class Cursor : Actor
    {
        public bool CursorEnabled { get; set; }
        public float LineWidth = 4.0f;
        public Color IdleLineColor = Color.white;
        public Color HoverLineColor = Color.yellow;
        public Vector2 CursorPosition { get; private set; }
        private Rect cursorBounds = new Rect();
        private Color lineColor = Color.white;
        private VectorLine line;

        void OnEnable()
        {
            TileMapFactory.OnGenerationComplete += SetCursorBounds;
        }

        void OnDestroy()
        {
            TileMapFactory.OnGenerationComplete -= SetCursorBounds;
        }

        void Awake()
        {
            InitializeLine();
        }

        void Update()
        {
            if (CursorEnabled)
            {
                SetCursorPosition();
                GenerateLine();
                CheckForCollisions<Tile>(layerMasks.TileMask);
                CheckForCollisions<Unit>(layerMasks.UnitMask);
                CheckForCollisions<PathfindingNode>(layerMasks.PathfindingMask);
            }
            else
                HideLine();
        }

        public Vector2 Coordinates
        {
             get { return Algorithms.ConvertPositionToWorldCoordinates(Input.mousePosition); }
        }

        private void SetCursorPosition()
        {
            CursorPosition = new Vector2((int)Coordinates.x, (int)Coordinates.y);
            RestrictCursorPositionToBounds();
        }

        private void SetCursorBounds(int areaWidth, int areaHeight)
        {
            cursorBounds = new Rect();

            cursorBounds.x = 0.0f;
            cursorBounds.y = 0.0f;
            cursorBounds.width = areaWidth;
            cursorBounds.height = areaHeight;
        }

        public void RestrictCursorPositionToBounds()
        {
            Vector2 newPosition = new Vector2(
                Mathf.Clamp(CursorPosition.x, cursorBounds.x, cursorBounds.width),
                Mathf.Clamp(CursorPosition.y, cursorBounds.y, cursorBounds.height));

            CursorPosition = new Vector3(newPosition.x, newPosition.y, -10.0f);
            gameObject.transform.position = CursorPosition;
        }

        public override void HandleCollision<T>(GameObject collidedObject)
        {
            if (collidedObject != null)
            {
                /*
                // Handle Tile Collisions
                if (typeof(T) == typeof(Tile))
                */

                // Handle Unit Collisions
                if (typeof(T) == typeof(Unit))
                    lineColor = HoverLineColor;
                else
                    lineColor = IdleLineColor;
                    
                /*
                // Handle Pathfinding Collisions
                if (typeof(T) == typeof(PathfindingNode))
                */
            }
        }

        private void InitializeLine()
        {
            line = new VectorLine("Mouse Cursor", new Vector3[5], null, LineWidth, LineType.Continuous, Joins.Weld);
            VectorLine.canvas3D.sortingLayerName = "Pathfinding";
            VectorLine.canvas3D.sortingOrder = 1;
        }

        private void GenerateLine()
        {
            if (line.active == false)
                ShowLine();

            line.MakeRect(new Rect(CursorPosition.x, CursorPosition.y, 1.0f, 1.0f));
            line.SetColor(lineColor);
            line.Draw3D();
        }

        private void ShowLine()
        {
            if (line != null)
                line.active = true;
        }

        private void HideLine()
        {
            if (line != null)
                line.active = false;
        }
    }
}