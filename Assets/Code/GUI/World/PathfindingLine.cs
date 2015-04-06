using Assets.Code.Entities.Pathfinding;
using Assets.Code.Libraries;
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
            Pathfinder.OnPathGenerationComplete += Generate;
        }

        void OnDestroy()
        {
            Pathfinder.OnPathGenerationComplete -= Generate;
        }

        private void Generate(List<Vector2> path)
        {
            if (path != null)
            {
             
                Show();

                int numPoints = path.Count;
                if (Algorithms.IsNumberOdd(numPoints))
                    numPoints += 1;

                pathfindingLine = new VectorLine("Pathfinding Line", new Vector3[numPoints], null, 1.0f);

                for (int i = 0; i < path.Count; i++)
                    pathfindingLine.points3[i] = path[i];

                VectorLine.canvas3D.pixelPerfect = true;
                VectorLine.canvas3D.sortingLayerName = "Pathfinding";
                VectorLine.canvas3D.sortingOrder = 1;

                pathfindingLine.SetColor(Color.white);
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