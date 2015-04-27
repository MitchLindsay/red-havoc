using Assets.Code.Actors;
using Assets.Code.Controllers;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.Events
{
    public class DisableInputEvent : Event
    {
        private InputHandler inputHandler;

        public DisableInputEvent(EventID eventID, object sender, EventArgs<InputHandler> e) : base(eventID, sender, e)
        {
            this.inputHandler = e.Value;
        }

        public override IEnumerator Execute()
        {
            inputHandler.DisableInput();
            yield return null;
        }

        public override IEnumerator Undo()
        {
            yield return null;
        }
    }
}