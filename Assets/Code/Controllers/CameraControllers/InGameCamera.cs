using Assets.Code.TileMaps.Generators;
using UnityEngine;

namespace Assets.Code.Controllers.CameraControllers
{
    public class InGameCamera : MonoBehaviour
    {
        // Flag to determine whether camera dragging is allowed
        private bool dragEnabled = false;

        // Speed at which the camera moves when the player drags the screen
        // Edited through the Unity interface
        public float DragSpeed = 0.2f;

        // Amount to move the camera when dragged on the x and y axes
        private float dragAmountX = 0.0f;
        private float dragAmountY = 0.0f;

        // Area in which the camera is allowed to move within
        private Rect movementBounds = new Rect();

        // Position of the camera restricted to the movement bounds
        private Vector3 restrictedCameraPosition = Vector3.zero;

        // Listen for events when object is created
        void OnEnable()
        {
            TileMapGenerator.OnGenerationComplete += SetMovementBounds;
        }

        // Stop listening for events if object is destroyed
        void OnDestroy()
        {
            TileMapGenerator.OnGenerationComplete -= SetMovementBounds;
        }

        void Start()
        {
            restrictedCameraPosition = Camera.main.transform.position;
        }

        void Update()
        {
            if (dragEnabled)
            {
                if (Input.GetMouseButton(1))
                {
                    dragAmountX = Input.GetAxis("Mouse X") * DragSpeed;
                    dragAmountY = Input.GetAxis("Mouse Y") * DragSpeed;

                    restrictedCameraPosition.x -= dragAmountX;
                    restrictedCameraPosition.y -= dragAmountY;
                }

                RestrictCameraMovement();
            }
        }

        // Sets the movement bounds of the camera given a width and height of the area
        private void SetMovementBounds(int areaWidth, int areaHeight)
        {
            movementBounds = new Rect();

            movementBounds.x = 0.0f;
            movementBounds.y = 0.0f;
            movementBounds.width = areaWidth;
            movementBounds.height = areaHeight;

            // Move the camera within the bounds initially
            RestrictCameraMovement();

            // Allow camera dragging
            dragEnabled = true;
        }

        // Apply the movement bounds to restrict the camera's position
        private void RestrictCameraMovement()
        {
            restrictedCameraPosition.x = Mathf.Clamp(restrictedCameraPosition.x, movementBounds.x, movementBounds.width);
            restrictedCameraPosition.y = Mathf.Clamp(restrictedCameraPosition.y, movementBounds.y, movementBounds.height);
            restrictedCameraPosition.z = -10.0f;

            Camera.main.transform.position = restrictedCameraPosition;
        }
    }
}