using Assets.Code.Actors;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.UnitActions
{
    public class UnitAttackAction : UnitAction
    {
        private Faction activeFaction;
        private Unit selectedUnit;
        private Unit targetUnit;

        public UnitAttackAction(EventID eventID, object sender, EventArgs<Faction, Unit, Unit> e) : base(eventID, sender, e)
        {
            this.activeFaction = e.Value;
            this.selectedUnit = e.Value2;
            this.targetUnit = e.Value3;
        }

        public override IEnumerator Execute()
        {
            activeFaction.DeactivateUnit(selectedUnit);
            Debug.Log(selectedUnit.Name + " attacked " + targetUnit.Name);
            yield return null;
        }
    }
}
