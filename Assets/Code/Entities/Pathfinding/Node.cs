using UnityEngine;

namespace Assets.Code.Entities.Pathfinding
{
    [RequireComponent (typeof(SpriteRenderer))]
    public class Node : MonoBehaviour
    {
        public Color32 ColorTraversable = new Color32(61, 118, 229, 200);
        public Color32 ColorOutOfReach = new Color32(229, 61, 61, 200);
        public Color32 ColorNeutral = new Color32(255, 255, 255, 0);

        void Start()
        {
            SetColor(ColorNeutral);
        }

        public int MoveCost = 0;
        public bool Traversable = true;
        public bool CanBeGoal = true;

        private void SetColor(Color color)
        {
            gameObject.GetComponent<SpriteRenderer>().color = color;
        }
    }
}