using Assets.Code.TileMaps.Generators;
using UnityEngine;

namespace Assets.Code.Controllers.CameraControllers
{
    public class InGameCamera : MonoBehaviour
    {   
        // Event handler for when camera is being dragged
        public delegate void CameraDragHandler();
        public static event CameraDragHandler OnCameraDragStart;
        public static event CameraDragHandler OnCameraDragStop;

        // Flag to determine whether camera dragging is allowed
        private bool dragEnabled = true;

        // Speed at which the camera moves when the player drags the screen
        // Edited through the Unity interface
        public float DragSpeed = 0.4f;

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
                    DragCamera();

                // Call event since camera is no longer being dragged
                if (!Input.GetMouseButton(1) && OnCameraDragStop != null)
                    OnCameraDragStop();
            }

            RestrictCameraMovement();
        }

        // Sets the movement bounds of the camera given a width and height of the area
        private void SetMovementBounds(int areaWidth, int areaHeight)
        {
            movementBounds = new Rect();

            // Set the movement bounds
            movementBounds.x = 0.0f;
            movementBounds.y = 0.0f;
            movementBounds.width = areaWidth;
            movementBounds.height = areaHeight;
        }
        
        // Drag the camera to the specified location
        private void DragCamera()
        {
            // Call event since camera is being dragged
            if (OnCameraDragStart != null)
                OnCameraDragStart();

            // Get inputs from mouse, multiplied by the drag speed
            dragAmountX = Input.GetAxis("Mouse X") * DragSpeed;
            dragAmountY = Input.GetAxis("Mouse Y") * DragSpeed;

            // Apply the drag amount to the camera position
            restrictedCameraPosition.x -= dragAmountX;
            restrictedCameraPosition.y -= dragAmountY;
        }

        // Apply the movement bounds to restrict the camera's position
        private void RestrictCameraMovement()
        {
            // Clamp the camera position within the movement bounds
            restrictedCameraPosition.x = Mathf.Clamp(restrictedCameraPosition.x, movementBounds.x, movementBounds.width);
            restrictedCameraPosition.y = Mathf.Clamp(restrictedCameraPosition.y, movementBounds.y, movementBounds.height);
            // Since the game is in 2D, always keep the z axis as -10
            restrictedCameraPosition.z = -10.0f;

            // Move the actual camera position to the restricted position
            Camera.main.transform.position = restrictedCameraPosition;
        }
    }
}