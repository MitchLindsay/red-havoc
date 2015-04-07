using Assets.Code.Controllers;
using Assets.Code.Controllers.States;
using Assets.Code.Entities.Abstract;
using Assets.Code.Entities.Pathfinding;
using Assets.Code.Entities.Tiles;
using Assets.Code.Entities.Units;
using Assets.Code.Libraries;
using UnityEngine;
using Vectrosity;

namespace Assets.Code.GUI.World
{
    public class MouseCursor : Entity
    {
        public delegate void NodeHandler(GameObject gameObject);
        public static event NodeHandler OnMouseOverNode;
        public static event NodeHandler OnMouseClickNode;

        public delegate void TileHandler(GameObject gameObject);
        public static event TileHandler OnMouseOverTile;
        public static event TileHandler OnMouseClickTile;

        public delegate void UnitHandler(GameObject gameObject);
        public static event UnitHandler OnMouseOverUnit;
        public static event UnitHandler OnMouseClickUnit;

        [HideInInspector]
        public bool CursorEnabled = false;
        public Vector2 Coordinates { get; private set; }

        private VectorLine cursorHighlight = null;
        private Color cursorHighlightColor = Color.white;

        void OnEnable()
        {
            ChangeTurnState.OnStateEntry += DisableCursor;
            SelectUnitState.OnStateEntry += EnableCursor;
            InGameCamera.OnPanStart += DisableCursor;
            InGameCamera.OnPanStop += EnableCursor;
        }

        void OnDestroy()
        {
            ChangeTurnState.OnStateEntry -= DisableCursor;
            SelectUnitState.OnStateEntry -= EnableCursor;
            InGameCamera.OnPanStart -= DisableCursor;
            InGameCamera.OnPanStop -= EnableCursor;
        }

        void Awake()
        {
            InitializeCursorHighlight();
        }

        void Update()
        {
            if (CursorEnabled)
            {
                SetCoordinates(GetMouseCoordinates());
                DrawCursorHighlight();
                CheckForCollisions<Node>(NodeLayerMask);
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

            VectorLine.canvas3D.sortingLayerName = "Pathfinding";
            VectorLine.canvas3D.sortingOrder = 1;
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

            if (typeof(TEntity) == typeof(Node))
                HandleNodeCollisions(collidedGameObject);

            if (typeof(TEntity) == typeof(Tile))
                HandleTileCollision(collidedGameObject);
        }

        private void HandleNodeCollisions(GameObject collidedGameObject)
        {
            if (OnMouseOverNode != null)
                OnMouseOverNode(collidedGameObject);

            if (OnMouseClickNode != null && Input.GetMouseButton(0))
                OnMouseClickNode(collidedGameObject);
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
            {
                Unit unit = collidedGameObject.GetComponent<Unit>();

                if (unit != null)
                    cursorHighlightColor = Color.yellow;
                else
                    cursorHighlightColor = Color.white;
            }
            else
                cursorHighlightColor = Color.white;
        }
    }
}