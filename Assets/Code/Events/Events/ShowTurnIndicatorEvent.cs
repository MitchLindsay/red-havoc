using Assets.Code.Actors;
using Assets.Code.Controllers;
using Assets.Code.UI.Static;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.Events
{
    public class ShowTurnIndicatorEvent : Event
    {
        private TurnIndicator turnIndicator;
        private float showDuration;
        private Faction activeFaction;

        public ShowTurnIndicatorEvent(EventID eventID, object sender, EventArgs<TurnIndicator, float> e) : base(eventID, sender, e)
        {
            this.turnIndicator = e.Value;
            this.showDuration = e.Value2;
        }

        public override IEnumerator Execute()
        {
            this.activeFaction = TurnHandler.Instance.ActiveFaction;
            Debug.Log("Displaying Turn Indicator for " + showDuration + " seconds.");

            if (activeFaction != null)
            {
                Debug.Log("Active Faction: " + activeFaction.Name);

                turnIndicator.SetColor(turnIndicator.Panel,
                    new Color(
                        activeFaction.UnitColor.r,
                        activeFaction.UnitColor.g,
                        activeFaction.UnitColor.b,
                        0.75f));

                turnIndicator.SetText(turnIndicator.Text, activeFaction.Name.ToUpper() + " TURN");
                turnIndicator.Show();
            }
            else
                Debug.Log("No faction to display!");

            yield return new WaitForSeconds(showDuration);

            turnIndicator.Hide();
            yield return null;
        }

        public override IEnumerator Undo()
        {
            yield return null;
        }
    }
}