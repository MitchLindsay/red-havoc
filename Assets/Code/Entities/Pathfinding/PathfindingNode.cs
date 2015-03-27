using UnityEngine;

namespace Assets.Code.Entities.Pathfinding
{
    [RequireComponent (typeof(SpriteRenderer))]
    public class PathfindingNode : MonoBehaviour
    {
        public Color32 ColorTraversable = new Color32(61, 118, 229, 255);
        public Color32 ColorOutOfReach = new Color32(229, 61, 61, 255);
        public Color32 ColorNeutral = new Color32(255, 255, 255, 0);

        public PathfindingNode Parent { get; set; }
        public int CostFromStart { get; set; }
        public int CostToGoal { get; set; }
        public int TotalCost { get; set; }
        public int MovementCost = 0;

        public void InitializeNode(int movementCost)
        {
            this.MovementCost = movementCost;
            ResetData();
        }

        public void ResetData()
        {
            Parent = null;
            CostFromStart = 0;
            CostToGoal = 0;
            TotalCost = 0;
        }
    }
}