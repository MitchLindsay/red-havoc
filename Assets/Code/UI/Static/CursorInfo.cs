using Assets.Code.Actors;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI.Static
{
    public class CursorInfo : Window
    {
        public Actors.Cursor Cursor;
        public Text Coordinates;

        void OnEnable()
        {
            Actors.Cursor.OnMouseOverNode += SetCursorInfo;
        }

        void OnDestroy()
        {
            Actors.Cursor.OnMouseOverNode -= SetCursorInfo;
        }

        private void SetCursorInfo(GameObject pathfindingNodeObject)
        {
            SetText(Coordinates, ((int)Cursor.Coordinates.x).ToString() + ", " + ((int)Cursor.Coordinates.y).ToString());

            if (pathfindingNodeObject != null)
                Show();
            else
                Hide();
        }
    }
}