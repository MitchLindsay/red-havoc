using Assets.Code.Actors;
using Assets.Code.Controllers;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.Events
{
    public class DeactivateUnitEvent : Event
    {
        private Actors.Cursor cursor;
        private Faction faction;

        public DeactivateUnitEvent(EventID eventID, object sender, EventArgs<Actors.Cursor, Faction> e) : base(eventID, sender, e)
        {
            this.cursor = e.Value;
            this.faction = e.Value2;
        }

        public override IEnumerator Execute()
        {
            Unit selectedUnit = cursor.SelectedUnit;
            faction.DeactivateUnit(selectedUnit);

            yield return null;
        }

        public override IEnumerator Undo()
        {
            yield return null;
        }
    }
}