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
        private Unit unit;

        public ShowMovementAreaEvent(EventID eventID, object sender, EventArgs<Pathfinder, Actors.Cursor> e) : base(eventID, sender, e)
        {
            this.pathfinder = e.Value;
            this.cursor = e.Value2;
        }

        public override IEnumerator Execute()
        {
            if (pathfinder != null)
            {
                if (unit == null)
                {
                    unit = cursor.SelectedUnit;

                    if (unit != null)
                        pathfinder.ShowArea(unit, unit.Movement.ModifiedValue);
                }
            }

            yield return null;
        }

        public override IEnumerator Undo()
        {
            pathfinder.HideArea();
            yield return null;
        }
    }
}