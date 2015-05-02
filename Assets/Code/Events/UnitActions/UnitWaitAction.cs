using Assets.Code.Actors;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.UnitActions
{
    public class UnitWaitAction : UnitAction
    {
        private Faction activeFaction;
        private Unit selectedUnit;

        public UnitWaitAction(EventID eventID, object sender, EventArgs<Faction, Unit> e) : base(eventID, sender, e)
        {
            this.activeFaction = e.Value;
            this.selectedUnit = e.Value2;
        }

        public override IEnumerator Execute()
        {
            activeFaction.DeactivateUnit(selectedUnit);
            Debug.Log(selectedUnit.Name + " waited");
            yield return null;
        }
    }
}