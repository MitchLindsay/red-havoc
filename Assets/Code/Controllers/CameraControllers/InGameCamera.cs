using Assets.Code.GUI.WorldSpace;
using Assets.Code.TileMaps.Generators;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Controllers.CameraControllers
{
    public class InGameCamera : MonoBehaviour
    {   
        public delegate void CameraDragHandler();
        public delegate void CameraPanHandler();
        public static event CameraDragHandler OnCameraDragStart;
        public static event CameraDragHandler OnCameraDragStop;
        public static event CameraPanHandler OnPanStart;
        public static event CameraPanHandler OnPanStop;

        private bool dragEnabled = true;
        private float dragAmountX = 0.0f;
        private float dragAmountY = 0.0f;

        public float DragSpeed = 0.4f;

        private Rect movementBounds = new Rect();
        private Vector3 restrictedCameraPosition = Vector3.zero;

        void OnEnable()
        {
            TileMapGenerator.OnGenerationComplete += SetMovementBounds;
            MouseCursor.OnMouseClickUnit += SetFocusToGameObject;
        }

        void OnDestroy()
        {
            TileMapGenerator.OnGenerationComplete -= SetMovementBounds;
            MouseCursor.OnMouseClickUnit -= SetFocusToGameObject;
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

                if (!Input.GetMouseButton(1) && OnCameraDragStop != null)
                    OnCameraDragStop();
            }

            RestrictCameraMovement();
        }

        private void SetMovementBounds(int areaWidth, int areaHeight)
        {
            movementBounds = new Rect();

            movementBounds.x = 0.0f;
            movementBounds.y = 0.0f;
            movementBounds.width = areaWidth;
            movementBounds.height = areaHeight;
        }
        
        private void DragCamera()
        {
            if (OnCameraDragStart != null)
                OnCameraDragStart();

            dragAmountX = Input.GetAxis("Mouse X") * DragSpeed;
            dragAmountY = Input.GetAxis("Mouse Y") * DragSpeed;
            // TODO: Add inverted controls

            restrictedCameraPosition.x -= dragAmountX;
            restrictedCameraPosition.y -= dragAmountY;
        }

        private void RestrictCameraMovement()
        {
            restrictedCameraPosition.x = Mathf.Clamp(restrictedCameraPosition.x, movementBounds.x, movementBounds.width);
            restrictedCameraPosition.y = Mathf.Clamp(restrictedCameraPosition.y, movementBounds.y, movementBounds.height);
            restrictedCameraPosition.z = -10.0f;

            Camera.main.transform.position = restrictedCameraPosition;
        }

        private void SetFocusToGameObject(GameObject gameObject)
        {
            if (gameObject != null)
            {
                int x = (int)gameObject.transform.position.x;
                int y = (int)gameObject.transform.position.y;

                Pan(new Vector2(x, y), 0.5f);
            }
        }

        private void Pan(Vector2 destination, float duration)
        {
            dragEnabled = false;

            if (OnPanStart != null)
                OnPanStart();

            Job panJob = Job.Make(PanCoroutine(destination, duration), true);

            panJob.JobComplete += (wasKilled) =>
            {
                if (OnPanStart != null)
                {
                    dragEnabled = true;
                    OnPanStop();
                }
            };
        }

        private IEnumerator PanCoroutine(Vector2 destination, float duration)
        {
            restrictedCameraPosition = Camera.main.transform.position;

            float timeElapsed = 0.0f;
            Vector3 startPosition = restrictedCameraPosition;

            while (timeElapsed < 1.0f)
            {
                timeElapsed += Time.deltaTime * (Time.timeScale / duration);
                restrictedCameraPosition = Vector3.Lerp(startPosition, destination, timeElapsed);

                RestrictCameraMovement();
                yield return null;
            }
        }
    }
}