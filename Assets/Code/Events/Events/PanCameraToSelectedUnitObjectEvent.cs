﻿using Assets.Code.Actors;
using Assets.Code.Controllers;
using Assets.Code.UI;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.Events
{
    public class PanCameraToSelectedUnitObjectEvent : Event
    {
        private CameraHandler cameraHandler;
        private Actors.Cursor cursor;
        private Unit unit;
        private Vector2 origin;
        private Vector2 destination;
        private float panDuration;

        public PanCameraToSelectedUnitObjectEvent(EventID eventID, object sender, EventArgs<CameraHandler, Actors.Cursor, float> e) : base(eventID, sender, e)
        {
            this.cameraHandler = e.Value;
            this.cursor = e.Value2;
            this.panDuration = e.Value3;
            this.origin = cameraHandler.CameraPositionWithinBounds;
        }

        public override IEnumerator Execute()
        {
            if (unit == null)
            {
                unit = cursor.SelectedUnit;
                destination = unit.gameObject.transform.position;
            }

            if (destination == origin)
                panDuration = 0.0f;

            cameraHandler.SetCameraPosition(Camera.main.transform.position);
            origin = cameraHandler.CameraPositionWithinBounds;

            float timeElapsed = 0.0f;
            while (timeElapsed < panDuration)
            {
                timeElapsed += Time.deltaTime * (Time.timeScale / cameraHandler.MoveSpeed);
                cameraHandler.SetCameraPosition(Vector2.Lerp(origin, destination, timeElapsed));
                yield return null;
            }
        }

        public override IEnumerator Undo()
        {
            if (destination == origin)
                yield return null;

            if (unit == null)
            {
                unit = cursor.SelectedUnit;
                destination = unit.gameObject.transform.position;
            }

            cameraHandler.SetCameraPosition(Camera.main.transform.position);

            float timeElapsed = 0.0f;
            while (timeElapsed < 1.0f)
            {
                timeElapsed += Time.deltaTime * (Time.timeScale / cameraHandler.MoveSpeed);
                cameraHandler.SetCameraPosition(Vector2.Lerp(destination, origin, timeElapsed));
                yield return null;
            }
        }
    }
}