using Assets.Code.Controllers;
using Assets.Code.Events;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.Events
{
    public class PanCameraToPositionEvent : Event
    {
        private CameraHandler controller;
        private Vector2 origin;
        private Vector2 destination;
        private float speed;

        public PanCameraToPositionEvent(EventID eventID, object sender, EventArgs<Vector2, float> e) : base(eventID, sender, e)
        {
            this.controller = (CameraHandler)sender;
            this.origin = controller.CameraPositionWithinBounds;
            this.destination = e.Value;
            this.speed = e.Value2;
        }

        public override IEnumerator Execute()
        {
            if (destination == origin)
                yield return null;

            controller.SetCameraPosition(Camera.main.transform.position);
            origin = controller.CameraPositionWithinBounds;

            float timeElapsed = 0.0f;
            while (timeElapsed < 1.0f)
            {
                timeElapsed += Time.deltaTime * (Time.timeScale / speed);
                controller.SetCameraPosition(Vector2.Lerp(origin, destination, timeElapsed));
                yield return null;
            }
        }

        public override IEnumerator Undo()
        {
            if (destination == origin)
                yield return null;

            controller.SetCameraPosition(Camera.main.transform.position);

            float timeElapsed = 0.0f;
            while (timeElapsed < 1.0f)
            {
                timeElapsed += Time.deltaTime * (Time.timeScale / speed);
                controller.SetCameraPosition(Vector2.Lerp(destination, origin, timeElapsed));
                yield return null;
            }
        }
    }
}