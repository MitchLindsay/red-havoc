using Assets.Code.Actors;
using Assets.Code.Controllers;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.Events
{
    public class EnableInputEvent : Event
    {
        private InputHandler inputHandler;

        public EnableInputEvent(EventID eventID, object sender, EventArgs<InputHandler> e) : base(eventID, sender, e)
        {
            this.inputHandler = e.Value;
        }

        public override IEnumerator Execute()
        {
            inputHandler.EnableInput();
            yield return null;
        }

        public override IEnumerator Undo()
        {
            yield return null;
        }
    }
}