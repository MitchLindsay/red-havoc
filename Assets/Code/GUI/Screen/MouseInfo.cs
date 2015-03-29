using Assets.Code.Generic.GUI.Abstract;
using Assets.Code.GUI.World;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Entities.GUI.Screen
{
    public class MouseInfo : InfoPanel
    {
        public MouseCursor MouseCursor;

        public Text Coordinates;
        public Image CursorIcon;

        void Update()
        {
            SetInfo();
        }

        public override void SetInfo()
        {
            if (PanelObject != null)
            {
                if (MouseCursor != null)
                {
                    ShowInfo();
                    SetTextElement(Coordinates, MouseCursor.Coordinates.x.ToString() + ", " + MouseCursor.Coordinates.y.ToString());
                }
                else
                    HideInfo();
            }
        }
    }
}