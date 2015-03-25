using Assets.Code.Generic;
using Assets.Code.TileMaps.Entities;
using Assets.Code.Units.Entities;
using UnityEngine;
using Vectrosity;

namespace Assets.Code.GUI.WorldSpace
{
    public class MouseCursor : Entity
    {
        public delegate void MouseOverHandler(GameObject gameObject);
        public static event MouseOverHandler OnMouseOverTile;
        public static event MouseOverHandler OnMouseOverUnit;

        public Vector2 Coordinates { get; private set; }

        private VectorLine cursorHighlight = null;
        private Color cursorHighlightColor = Color.white;

        void Start()
        {
            InitializeCursorHighlight();
        }

        void Update()
        {
            SetCoordinates();
            DrawCursorHighlight();
            CheckForCollisions<Tile>(TileLayerMask);
            CheckForCollisions<Unit>(UnitLayerMask);
        }

        private void InitializeCursorHighlight()
        {
            cursorHighlight = new VectorLine("Cursor Highlight", new Vector3[5], null, 4.0f, LineType.Continuous, Joins.Weld);

            VectorLine.canvas3D.sortingLayerName = "Tile";
            VectorLine.canvas3D.sortingOrder = 2;
        }

        private void SetCoordinates()
        {
            Coordinates = Algorithms.ConvertPositionToWorldCoordinates(Input.mousePosition);
            Coordinates = new Vector2((int)Coordinates.x, (int)Coordinates.y);
            gameObject.transform.position = Coordinates;
        }

        private void DrawCursorHighlight()
        {
            cursorHighlight.MakeRect(new Rect(Coordinates.x, Coordinates.y, 1.0f, 1.0f));
            cursorHighlight.SetColor(cursorHighlightColor);
            cursorHighlight.Draw3D();
        }

        public override void HandleCollision<TEntity>(GameObject collidedGameObject)
        {
            if (typeof(TEntity) == typeof(Tile))
            {
                if (OnMouseOverTile != null)
                    OnMouseOverTile(collidedGameObject);
            }

            if (typeof(TEntity) == typeof(Unit))
            {
                if (OnMouseOverUnit != null)
                    OnMouseOverUnit(collidedGameObject);

                if (collidedGameObject != null)
                    cursorHighlightColor = collidedGameObject.GetComponent<SpriteRenderer>().color;
                else
                    cursorHighlightColor = Color.white;
            }
        }
    }
}