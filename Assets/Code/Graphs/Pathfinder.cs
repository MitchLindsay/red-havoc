using Assets.Code.Actors;
using UnityEngine;

namespace Assets.Code.Graphs
{
    public class Pathfinder : Graph<PathfindingNode>
    {
        public Color32 ColorTraversable = new Color32(61, 118, 229, 150);
        public Color32 ColorOutOfReach = new Color32(229, 61, 61, 150);
        public Color32 ColorNeutral = new Color32(255, 255, 255, 0);
    }
}