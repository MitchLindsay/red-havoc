using Assets.Code.Actors;
using Assets.Code.Graphs;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.Events
{
    public class HideMovementAreaEvent : Event
    {
        private Pathfinder pathfinder;

        public HideMovementAreaEvent(EventID eventID, object sender, EventArgs<Pathfinder, Actors.Cursor> e) : base(eventID, sender, e)
        {
            this.pathfinder = e.Value;
        }

        public override IEnumerator Execute()
        {
            pathfinder.HideArea();
            yield return null;
        }

        public override IEnumerator Undo()
        {
            yield return null;
        }
    }
}