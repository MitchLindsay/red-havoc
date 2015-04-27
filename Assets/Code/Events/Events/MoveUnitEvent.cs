using Assets.Code.Actors;
using Assets.Code.Controllers;
using Assets.Code.Graphs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Events.Events
{
    public class MoveUnitEvent : Event
    {
        private Actors.Cursor cursor;
        private Pathfinder pathfinder;
        private float moveSpeed;

        public MoveUnitEvent(EventID eventID, object sender, EventArgs<Actors.Cursor, Pathfinder, float> e) : base(eventID, sender, e)
        {
            this.cursor = e.Value;
            this.pathfinder = e.Value2;
            this.moveSpeed = e.Value3;
        }

        public override IEnumerator Execute()
        {
            Unit selectedUnit = cursor.SelectedUnit;
            Vector2 unitPosition = selectedUnit.gameObject.transform.position;
            List<Vector2> path = pathfinder.LastPathGenerated;

            for (int i = 0; i < path.Count; i++)
            {
                float timeElapsed = 0.0f;
                Vector3 destination = path[i] - new Vector2(0.5f, 0.5f);
                Vector3 startPosition = unitPosition;

                while (timeElapsed < 1.0f)
                {
                    timeElapsed += Time.deltaTime * (Time.timeScale / moveSpeed);
                    unitPosition = Vector3.Lerp(startPosition, destination, timeElapsed);

                    selectedUnit.gameObject.transform.position = unitPosition;
                    yield return null;
                }
            }

            yield return null;
        }

        public override IEnumerator Undo()
        {
            List<Vector2> path = pathfinder.LastPathGenerated;
            Unit selectedUnit = cursor.SelectedUnit;

            if (path != null && path.Count > 0)
                selectedUnit.gameObject.transform.position = path[0] - new Vector2(0.5f, 0.5f);

            yield return null;
        }
    }
}