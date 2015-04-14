using Assets.Code.Actors;
using Assets.Code.Controllers;
using Assets.Code.UI;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.Events
{
    public class PanCameraToGameObjectEvent : Event
    {
        private CameraHandler cameraHandler;
        private GameObject targetGameObject;
        private Vector2 origin;
        private Vector2 destination;
        private float panDuration;

        public PanCameraToGameObjectEvent(EventID eventID, object sender, EventArgs<CameraHandler, GameObject, float> e) : base(eventID, sender, e)
        {
            this.cameraHandler = e.Value;
            this.targetGameObject = e.Value2;
            this.panDuration = e.Value3;
            this.origin = cameraHandler.CameraPositionWithinBounds;

            if (targetGameObject != null)
                this.destination = targetGameObject.transform.position;
        }

        public override IEnumerator Execute()
        {
            if (targetGameObject == null)
            {
                targetGameObject = TurnHandler.Instance.ActiveFaction.Units[0].gameObject;
                destination = targetGameObject.transform.position;
            }

            if (destination == origin)
                yield return null;

            Debug.Log("Panning camera to " + targetGameObject + " at " + destination + " from " + origin);

            cameraHandler.SetCameraPosition(Camera.main.transform.position);
            origin = cameraHandler.CameraPositionWithinBounds;

            float timeElapsed = 0.0f;
            while (timeElapsed < panDuration)
            {
                timeElapsed += Time.deltaTime * (Time.timeScale / cameraHandler.moveSpeed);
                cameraHandler.SetCameraPosition(Vector2.Lerp(origin, destination, timeElapsed));
                yield return null;
            }
        }

        public override IEnumerator Undo()
        {
            if (destination == origin)
                yield return null;

            cameraHandler.SetCameraPosition(Camera.main.transform.position);

            float timeElapsed = 0.0f;
            while (timeElapsed < 1.0f)
            {
                timeElapsed += Time.deltaTime * (Time.timeScale / cameraHandler.moveSpeed);
                cameraHandler.SetCameraPosition(Vector2.Lerp(destination, origin, timeElapsed));
                yield return null;
            }
        }
    }
}