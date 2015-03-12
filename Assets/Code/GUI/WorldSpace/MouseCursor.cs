using Assets.Code.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.GUI.WorldSpace
{
    // MouseCursor.cs
    public class MouseCursor : MonoBehaviour
    {
        // Current location of the mouse cursor in world space
        public Vector2 Coordinates { get; private set; }
        // Current X coordinate of the mouse cursor in world space
        public int XCoordinateInt { get; private set; }
        // Current Y coordinte of the mouse cursor in world space
        public int YCoordianteInt { get; private set; }
        // GUI label of the mouse cursor's coordinates, edited through the Unity interface
        public Text CoordinatesLabel;

        void Update()
        {
            UpdateCoordinates(Input.mousePosition);
            UpdateCoordinatesLabel();
        }
         
        // Updates the mouse cursor's coordinates, using generic calculations
        private void UpdateCoordinates(Vector2 mousePosition)
        {
            Coordinates = Algorithms.ConvertPositionToWorldCoordinates(mousePosition);

            XCoordinateInt = (int)Coordinates.x;
            YCoordianteInt = (int)Coordinates.y;
        }

        // Updates the mouse cursor's coordinates text
        private void UpdateCoordinatesLabel()
        {
            CoordinatesLabel.text = "Coordinates: " + "(" + XCoordinateInt.ToString() +", " + YCoordianteInt.ToString() + ")";
        }
    }
}