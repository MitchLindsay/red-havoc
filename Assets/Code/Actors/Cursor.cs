using Assets.Code.Factories;
using Assets.Code.Generic;
using UnityEngine;
using Vectrosity;

namespace Assets.Code.Actors
{
    public class Cursor : Actor
    {
        public delegate void UnitHandler(GameObject gameObject);
        public delegate void NodeHandler(GameObject gameObject);
        public delegate void TileHandler(GameObject gameObject);

        public static event UnitHandler OnMouseOverUnit;
        public static event UnitHandler OnMouseClickUnit;
        public static event NodeHandler OnMouseOverNode;
        public static event NodeHandler OnMouseClickNode;
        public static event TileHandler OnMouseOverTile;
        public static event TileHandler OnMouseClickTile;

        public bool CursorEnabled { get; set; }
        public float LineWidth = 4.0f;
        public Color IdleLineColor = Color.white;
        public Color HoverLineColor = Color.yellow;
        public Vector2 CursorPosition { get; private set; }
        public GameObject LastClickedUnitObject { get; private set; }
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
            LastClickedUnitObject = null;
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
            if (typeof(T) == typeof(Unit))
                HandleUnitCollision(collidedObject);

            if (typeof(T) == typeof(PathfindingNode))
                HandleNodeCollisions(collidedObject);

            if (typeof(T) == typeof(Tile))
                HandleTileCollision(collidedObject);
        }

        private void HandleUnitCollision(GameObject collidedObject)
        {
            if (OnMouseOverUnit != null)
                OnMouseOverUnit(collidedObject);

            if (OnMouseClickUnit != null && Input.GetMouseButtonDown(0))
            {
                OnMouseClickUnit(collidedObject);

                if (collidedObject != null)
                {
                    Unit unit = collidedObject.GetComponent<Unit>();
                    if (unit != null)
                        LastClickedUnitObject = collidedObject;
                }
            }

            if (collidedObject != null)
            {
                Unit unit = collidedObject.GetComponent<Unit>();

                if (unit != null)
                    lineColor = HoverLineColor;
                else
                    lineColor = IdleLineColor;
            }
            else
                lineColor = IdleLineColor;
        }

        private void HandleNodeCollisions(GameObject collidedObject)
        {
            if (OnMouseOverNode != null)
                OnMouseOverNode(collidedObject);

            if (OnMouseClickNode != null && Input.GetMouseButtonDown(0))
                OnMouseClickNode(collidedObject);
        }

        private void HandleTileCollision(GameObject collidedObject)
        {
            if (OnMouseOverTile != null)
                OnMouseOverTile(collidedObject);

            if (OnMouseClickTile != null && Input.GetMouseButtonDown(0))
                OnMouseClickTile(collidedObject);
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