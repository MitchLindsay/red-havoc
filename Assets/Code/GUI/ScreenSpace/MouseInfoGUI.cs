using Assets.Code.GUI.WorldSpace;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.GUI.ScreenSpace
{
    public class MouseInfoGUI : MonoBehaviour
    {
        // Referenced GameObjects
        public MouseCursor MouseCursor;

        // GUI elements, set in Unity interface
        public Text Text_Mouse_Coordinates;
        public Image Image_Mouse_Coordinates;

        void Update()
        {
            if (MouseCursor != null)
            {
                SetText(MouseCursor.XCoordinateInt, MouseCursor.YCoordinateInt);
                SetColor(MouseCursor.CursorColor);
            }
        }

        private void SetText(int x, int y)
        {
            if (Text_Mouse_Coordinates != null)
                Text_Mouse_Coordinates.text = x + ", " + y;
        }

        private void SetColor(Color color)
        {
            if (Image_Mouse_Coordinates != null && color != null)
                Image_Mouse_Coordinates.color = color;
        }
    }
}