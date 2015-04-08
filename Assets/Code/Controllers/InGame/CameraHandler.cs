﻿using Assets.Code.Controllers.StateMachine.States;
using Assets.Code.Entities.Pathfinding;
using Assets.Code.Entities.Tiles;
using Assets.Code.Entities.Units;
using Assets.Code.Libraries;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Controllers.InGame
{
    public class CameraHandler : MonoBehaviour
    {
        public delegate void CameraPanHandler();
        public static event CameraPanHandler OnPanStart;
        public static event CameraPanHandler OnPanStop;

        public delegate void CameraDragHandler();
        public static event CameraDragHandler OnCameraDragStart;
        public static event CameraDragHandler OnCameraDragStop;

        public float PanSpeed = 0.4f;
        public float DragSpeed = 0.4f;
        
        private bool dragEnabled = false;
        private float dragAmountX = 0.0f;
        private float dragAmountY = 0.0f;

        private Rect movementBounds = new Rect();
        private Vector3 restrictedCameraPosition = Vector3.zero;
        private Vector2 nextPanLocation = Vector2.zero;

        void OnEnable()
        {
            TileMapFactory.OnGenerateComplete += SetMovementBounds;
            SelectUnitState.OnStateEntry += EnableDrag;
            SelectUnitState.OnUnitSelect += PanToGameObject;
            Pathfinder.OnPathGenerateComplete += SetNextPanLocation;
            Unit.OnMoveStart += PanToNextLocation;
            Unit.OnMoveStop += EnableDrag;
            Unit.OnMoveCancel += PanToGameObject;
        }

        void OnDestroy()
        {
            TileMapFactory.OnGenerateComplete -= SetMovementBounds;
            SelectUnitState.OnStateEntry -= EnableDrag;
            SelectUnitState.OnUnitSelect -= PanToGameObject;
            Pathfinder.OnPathGenerateComplete -= SetNextPanLocation;
            Unit.OnMoveStart -= PanToNextLocation;
            Unit.OnMoveStop -= EnableDrag;
            Unit.OnMoveCancel -= PanToGameObject;
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

        private void Pan(Vector2 destination, float speed, bool reenableDrag)
        {
            DisableDrag();

            if (OnPanStart != null)
                OnPanStart();

            Job panJob = Job.Make(PanCoroutine(destination, speed), true);

            panJob.JobComplete += (wasKilled) =>
            {
                if (reenableDrag)
                    EnableDrag();

                if (OnPanStop != null)
                    OnPanStop();
            };
        }

        private IEnumerator PanCoroutine(Vector2 destination, float speed)
        {
            restrictedCameraPosition = Camera.main.transform.position;

            float timeElapsed = 0.0f;
            Vector3 startPosition = restrictedCameraPosition;

            while (timeElapsed < 1.0f)
            {
                timeElapsed += Time.deltaTime * (Time.timeScale / speed);
                restrictedCameraPosition = Vector3.Lerp(startPosition, destination, timeElapsed);

                RestrictCameraMovement();
                yield return null;
            }
        }

        private void PanToGameObject(GameObject gameObject)
        {
            if (gameObject != null)
            {
                int x = (int)gameObject.transform.position.x;
                int y = (int)gameObject.transform.position.y;

                if (x != (int)Camera.main.transform.position.x || y != (int)Camera.main.transform.position.y)
                    Pan(new Vector2(x, y), PanSpeed, true);
                else
                {
                    if (OnPanStop != null)
                        OnPanStop();
                }
            }
        }

        private void PanToPosition(Vector2 position)
        {
            int x = (int)position.x;
            int y = (int)position.y;

            if (x != (int)Camera.main.transform.position.x || y != (int)Camera.main.transform.position.y)
                Pan(new Vector2(x, y), PanSpeed, false);
            else
            {
                if (OnPanStop != null)
                    OnPanStop();
            }
        }

        private void SetNextPanLocation(List<Vector2> positions)
        {
            if (positions != null && positions.Count > 0)
                nextPanLocation = positions[positions.Count - 1];
        }

        private void PanToNextLocation()
        {
            PanToPosition(nextPanLocation);
        }

        private void EnableDrag()
        {
            dragEnabled = true;
        }

        private void DisableDrag()
        {
            dragEnabled = false;
        }
    }
}