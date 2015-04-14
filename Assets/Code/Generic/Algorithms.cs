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

        public static int GetManhattanDistance(Vector2 startPosition, Vector2 endPosition)
        {
            int startX = (int)startPosition.x;
            int startY = (int)startPosition.y;

            int endX = (int)endPosition.x;
            int endY = (int)endPosition.y;

            return (Mathf.Abs(startX - endX) + Mathf.Abs(startY - endY));
        }

        public static bool IsNumberOdd(int number)
        {
            if (number > 0 && number % 2 != 0)
                return true;
            return false;
        }
    }
}