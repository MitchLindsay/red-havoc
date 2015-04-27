using Assets.Code.Actors;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.Events
{
    public class SelectUnitEvent : Event
    {
        private Actors.Cursor cursor;

        public SelectUnitEvent(EventID eventID, object sender, EventArgs<Actors.Cursor> e) : base(eventID, sender, e)
        {
            this.cursor = e.Value;
        }

        public override IEnumerator Execute()
        {
            if (cursor.LastClickedUnitObject != null && cursor.LastClickedUnitObject.GetComponent<Unit>() != null)
                cursor.SelectUnit(cursor.LastClickedUnitObject.GetComponent<Unit>());
            yield return null;
        }

        public override IEnumerator Undo()
        {
            yield return null;
        }
    }
}