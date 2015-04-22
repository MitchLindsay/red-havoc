using Assets.Code.Actors;
using Assets.Code.Graphs;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.Events
{
    public class ShowMovementAreaEvent : Event
    {
        private Pathfinder pathfinder;
        private Actors.Cursor cursor;
        private GameObject unitObject;

        public ShowMovementAreaEvent(EventID eventID, object sender, EventArgs<Pathfinder, Actors.Cursor> e) : base(eventID, sender, e)
        {
            this.pathfinder = e.Value;
            this.cursor = e.Value2;
        }

        public override IEnumerator Execute()
        {
            if (pathfinder != null)
            {
                if (unitObject == null)
                {
                    unitObject = cursor.LastClickedUnitObject;
                    Unit unit = unitObject.GetComponent<Unit>();

                    if (unit != null)
                    {
                        Debug.Log("Displaying Movement Area: " + unitObject);
                        pathfinder.ShowArea(unit, unit.Movement.ModifiedValue);
                    }
                else
                    Debug.Log("Can't display movement area, unit is null!");
                }
                else
                    Debug.Log("Can't display movement area, unit object is null!");
            }
            else
                Debug.Log("Pathfinder is null!");

            yield return null;
        }

        public override IEnumerator Undo()
        {
            pathfinder.HideArea();
            yield return null;
        }
    }
}