using Assets.Code.Controllers.StateMachine.States;
using Assets.Code.Entities.Pathfinding;
using Assets.Code.Entities.Units;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

namespace Assets.Code.GUI.World
{
    public class PathfindingLine : MonoBehaviour
    {
        private VectorLine pathfindingLine;

        void OnEnable()
        {
            Pathfinder.OnPathGenerateComplete += Generate;
            Unit.OnMoveStart += Hide;
            MoveUnitState.OnUnitDeselect += Hide;
        }

        void OnDestroy()
        {
            Pathfinder.OnPathGenerateComplete -= Generate;
            Unit.OnMoveStart -= Hide;
            MoveUnitState.OnUnitDeselect -= Hide;
        }

        void Start()
        {
            pathfindingLine = new VectorLine("Pathfinding Line", new Vector3[0], null, 3.0f, LineType.Continuous);

            VectorLine.canvas3D.pixelPerfect = true;
            VectorLine.canvas3D.sortingLayerName = "Pathfinding";
            VectorLine.canvas3D.sortingOrder = 1;

            pathfindingLine.SetColor(Color.white);
        }

        private void Generate(List<Vector2> path)
        {
            if (path != null)
            {
                Show();
                pathfindingLine.Resize(path.Count);

                for (int i = 0; i < path.Count; i++)
                {
                    path[i] += new Vector2(0.5f, 0.5f);
                    pathfindingLine.points3[i] = path[i];
                }

                pathfindingLine.Draw3DAuto();
            }
            else
                Hide();
        }

        private void Show()
        {
            if (pathfindingLine != null)
                pathfindingLine.active = true;
        }

        private void Hide()
        {
            if (pathfindingLine != null)
                pathfindingLine.active = false;
        }
    }
}