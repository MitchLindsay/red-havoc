using Assets.Code.Controllers;
using Assets.Code.Events;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.Events
{
    public class PanCameraToGameObjectEvent : Event
    {
        private CameraHandler controller;
        private Vector2 origin;
        private Vector2 destination;
        private GameObject targetGameObject;
        private float speed;

        public PanCameraToGameObjectEvent(EventID eventID, object sender, EventArgs<GameObject, float> e) : base(eventID, sender, e)
        {
            this.controller = (CameraHandler)sender;
            this.targetGameObject = e.Value;
            this.origin = controller.CameraPositionWithinBounds;
            this.destination = targetGameObject.transform.position;
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