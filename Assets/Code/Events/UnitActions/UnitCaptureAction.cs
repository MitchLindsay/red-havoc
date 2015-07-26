using Assets.Code.Actors;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.UnitActions
{
    public class UnitCaptureAction : UnitAction
    {
        private Faction activeFaction;
        private Unit selectedUnit;
        private Tile targetTile;

        public UnitCaptureAction(EventID eventID, object sender, EventArgs<Faction, Unit, Tile> e) : base(eventID, sender, e)
        {
            this.activeFaction = e.Value;
            this.selectedUnit = e.Value2;
            this.targetTile = e.Value3;
        }

        public override IEnumerator Execute()
        {
            activeFaction.DeactivateUnit(selectedUnit);
            Debug.Log(selectedUnit.Name + " captured " + targetTile.Name);
            yield return null;
        }
    }
}
