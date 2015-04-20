using Assets.Code.Actors;
using UnityEngine.UI;

namespace Assets.Code.UI.Static
{
    public class CursorInfo : Window
    {
        public Cursor Cursor;
        public Text Coordinates;

        void Update()
        {
            SetText(Coordinates, ((int)Cursor.Coordinates.x).ToString() + ", " + ((int)Cursor.Coordinates.y).ToString());
        }
    }
}