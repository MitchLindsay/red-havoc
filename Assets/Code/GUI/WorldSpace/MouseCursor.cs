using Assets.Code.Controllers.CameraControllers;
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
        public delegate void MouseClickHandler(GameObject gameObject);
        public static event MouseOverHandler OnMouseOverTile;
        public static event MouseOverHandler OnMouseOverUnit;
        public static event MouseClickHandler OnMouseClickTile;
        public static event MouseClickHandler OnMouseClickUnit;

        [HideInInspector]
        public bool CursorEnabled = false;
        public Vector2 Coordinates { get; private set; }

        private VectorLine cursorHighlight = null;
        private Color cursorHighlightColor = Color.white;

        void OnEnable()
        {
            InGameCamera.OnPanStart += DisableCursor;
            InGameCamera.OnPanStop += EnableCursor;
        }

        void OnDestroy()
        {
            InGameCamera.OnPanStart -= DisableCursor;
            InGameCamera.OnPanStop -= EnableCursor;
        }

        void Start()
        {
            InitializeCursorHighlight();
            EnableCursor();
        }

        void Update()
        {
            if (CursorEnabled)
            {
                SetCoordinates(GetMouseCoordinates());
                DrawCursorHighlight();
                CheckForCollisions<Tile>(TileLayerMask);
                CheckForCollisions<Unit>(UnitLayerMask);
            }
        }

        private void EnableCursor()
        {
            Cursor.visible = true;
            CursorEnabled = true;
            cursorHighlight.active = true;
        }

        private void DisableCursor()
        {
            Cursor.visible = false;
            CursorEnabled = false;
            cursorHighlight.active = false;
        }

        private void InitializeCursorHighlight()
        {
            cursorHighlight = new VectorLine("Cursor Highlight", new Vector3[5], null, 4.0f, LineType.Continuous, Joins.Weld);

            VectorLine.canvas3D.sortingLayerName = "Tile";
            VectorLine.canvas3D.sortingOrder = 2;
        }

        private Vector2 GetMouseCoordinates()
        {
            return Algorithms.ConvertPositionToWorldCoordinates(Input.mousePosition);
        }

        private void SetCoordinates(Vector2 inputCoordinates)
        {
            Coordinates = new Vector2((int)inputCoordinates.x, (int)inputCoordinates.y);
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
            if (typeof(TEntity) == typeof(Unit))
                HandleUnitCollision(collidedGameObject);

            if (typeof(TEntity) == typeof(Tile))
                HandleTileCollision(collidedGameObject);
        }

        private void HandleTileCollision(GameObject collidedGameObject)
        {
            if (OnMouseOverTile != null)
                OnMouseOverTile(collidedGameObject);

            if (OnMouseClickTile != null && Input.GetMouseButton(0))
                OnMouseClickTile(collidedGameObject);
        }

        private void HandleUnitCollision(GameObject collidedGameObject)
        {
            if (OnMouseOverUnit != null)
                OnMouseOverUnit(collidedGameObject);

            if (OnMouseClickUnit != null && Input.GetMouseButton(0))
            {
                if (collidedGameObject != null)
                    DisableCursor();

                OnMouseClickUnit(collidedGameObject);
            }

            if (collidedGameObject != null)
                cursorHighlightColor = collidedGameObject.GetComponent<SpriteRenderer>().color;
            else
                cursorHighlightColor = Color.white;
        }
    }
}