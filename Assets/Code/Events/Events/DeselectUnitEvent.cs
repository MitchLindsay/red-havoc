using Assets.Code.Actors;
using Assets.Code.Controllers;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.Events
{
    public class DeselectUnitEvent : Event
    {
        private Actors.Cursor cursor;

        public DeselectUnitEvent(EventID eventID, object sender, EventArgs<Actors.Cursor> e) : base(eventID, sender, e)
        {
            this.cursor = e.Value;
        }

        public override IEnumerator Execute()
        {
            cursor.DeselectUnit();
            yield return null;
        }

        public override IEnumerator Undo()
        {
            yield return null;
        }
    }
}