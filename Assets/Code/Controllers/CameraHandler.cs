using Assets.Code.Factories;
using Assets.Code.Generic;
using UnityEngine;

namespace Assets.Code.Controllers
{
    public class CameraHandler : Singleton<CameraHandler>
    {
        public float moveSpeed = 0.4f;
        public Vector3 CameraPositionWithinBounds { get; private set; }
        private Rect cameraBounds = new Rect();

        void OnEnable()
        {
            TileMapFactory.OnGenerationComplete += SetCameraBounds;
        }

        void OnDestroy()
        {
            TileMapFactory.OnGenerationComplete -= SetCameraBounds;
        }

        void Start()
        {
            SetCameraPosition(Camera.main.transform.position);
        }

        public void SetCameraPosition(Vector2 newPosition)
        {
            CameraPositionWithinBounds = newPosition;
            RestrictCameraPositionToBounds();
        }

        private void SetCameraBounds(int areaWidth, int areaHeight)
        {
            cameraBounds = new Rect();

            cameraBounds.x = 0.0f;
            cameraBounds.y = 0.0f;
            cameraBounds.width = areaWidth;
            cameraBounds.height = areaHeight;
        }

        public void RestrictCameraPositionToBounds()
        {
            Vector2 newPosition = new Vector2(
                Mathf.Clamp(CameraPositionWithinBounds.x, cameraBounds.x, cameraBounds.width),
                Mathf.Clamp(CameraPositionWithinBounds.y, cameraBounds.y, cameraBounds.height));

            CameraPositionWithinBounds = new Vector3(newPosition.x, newPosition.y, -10.0f);
            Camera.main.transform.position = CameraPositionWithinBounds;
        }
    }
}