using Assets.Code.Actors;
using Assets.Code.Controllers;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.Events
{
    public class EnableCursorEvent : Event
    {
        private InputHandler inputHandler;

        public EnableCursorEvent(EventID eventID, object sender, EventArgs<InputHandler> e) : base(eventID, sender, e)
        {
            this.inputHandler = e.Value;
        }

        public override IEnumerator Execute()
        {
            inputHandler.EnableCursor();
            yield return null;
        }

        public override IEnumerator Undo()
        {
            yield return null;
        }
    }
}