using Assets.Code.Generic;
using UnityEngine;

namespace Assets.Code.Controllers
{
    public class CameraHandler : Singleton<CameraHandler>
    {
        public float moveSpeed = 0.4f;
        public Rect CameraBounds { get; private set; }
        public Vector3 CameraPositionWithinBounds { get; private set; }

        void Awake()
        {
            CameraPositionWithinBounds = Camera.main.transform.position;
        }

        public void SetCameraPosition(Vector2 newPosition)
        {
            CameraPositionWithinBounds = newPosition;
            RestrictCameraPositionToBounds();
        }

        public void RestrictCameraPositionToBounds()
        {
            Vector2 newPosition = new Vector2(
                Mathf.Clamp(CameraPositionWithinBounds.x, CameraBounds.x, CameraBounds.width),
                Mathf.Clamp(CameraPositionWithinBounds.y, CameraBounds.y, CameraBounds.height));

            CameraPositionWithinBounds = newPosition;
            Camera.main.transform.position = CameraPositionWithinBounds;
        }
    }
}