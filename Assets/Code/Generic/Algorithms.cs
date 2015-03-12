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

        // Returns true if a number is even, false is a number is not
        public static bool IsNumberEven(int number)
        {
            if (number % 2 == 0)
                return true;
            return false;
        }
    }
}