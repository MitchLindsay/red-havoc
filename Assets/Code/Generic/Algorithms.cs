using UnityEngine;

namespace Assets.Code.Generic
{
    public static class Algorithms
    {
        public static Vector2 ConvertPositionToWorldCoordinates(Vector2 position)
        {
            return Camera.main.ScreenToWorldPoint(position);
        }

        public static Vector2 ConvertPositionToScreenCoordinates(Vector2 position)
        {
            return Camera.main.WorldToScreenPoint(position);
        }

        public static bool IsNumberOdd(int number)
        {
            if (number > 0 && number % 2 != 0)
                return true;
            return false;
        }
    }
}