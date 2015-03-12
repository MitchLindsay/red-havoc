using UnityEngine;

namespace Assets.Code.Generic
{
    // Algorithms.cs - Generic reusable calculations
    public static class Algorithms
    {
        // Returns the world coordinates given a location
        public static Vector2 ConvertPositionToWorldCoordinates(Vector2 position)
        {
            return Camera.main.ScreenToWorldPoint(position);
        }

        // Returns the screen coordinates given a location
        public static Vector2 ConvertPositionToScreenCoordinates(Vector2 position)
        {
            return Camera.main.WorldToScreenPoint(position);
        }
    }
}