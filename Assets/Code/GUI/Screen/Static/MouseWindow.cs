using Assets.Code.Generic.GUI.Abstract;
using Assets.Code.GUI.World;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.GUI.Screen.Static
{
    public class MouseWindow : Window
    {
        public MouseCursor MouseCursor;
        public Text Coordinates;
        public Image CursorIcon;

        private 

        void Update()
        {
            OnWindow();
        }

        public override void DisplayGUI()
        {
            SetText(Coordinates, MouseCursor.Coordinates.x.ToString() + ", " + MouseCursor.Coordinates.y.ToString());
        }
    }
}