using Assets.Code.Controllers;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.Events
{
    public class ChangeActiveFactionEvent : Event
    {
        private TurnHandler turnHandler;

        public ChangeActiveFactionEvent(EventID eventID, object sender, EventArgs<TurnHandler> e) : base(eventID, sender, e)
        {
            this.turnHandler = e.Value;
        }

        public override IEnumerator Execute()
        {
            turnHandler.ChangeTurns();
            Debug.Log("Changed turns");
            yield return new WaitForSeconds(1.0f);
        }

        public override IEnumerator Undo()
        {
            yield return null;
        }
    }
}