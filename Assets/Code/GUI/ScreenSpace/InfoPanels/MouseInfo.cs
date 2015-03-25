using Assets.Code.GUI.WorldSpace;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.GUI.ScreenSpace
{
    public class MouseInfo : InfoPanelController
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
            if (InfoPanel != null)
            {
                if (MouseCursor != null)
                {
                    InfoPanel.SetActive(true);

                    SetTextElement(Coordinates, MouseCursor.Coordinates.x.ToString() + ", " + MouseCursor.Coordinates.y.ToString());
                }
                else
                    InfoPanel.SetActive(false);
            }
        }
    }
}