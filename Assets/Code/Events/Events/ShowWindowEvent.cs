using Assets.Code.Actors;
using Assets.Code.UI.Interactable;
using Assets.Code.UI.Static;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.Events
{
    public class ShowWindowEvent : Event
    {
        private Window window;

        public ShowWindowEvent(EventID eventID, object sender, EventArgs<Window> e) : base(eventID, sender, e)
        {
            this.window = e.Value;
        }

        public override IEnumerator Execute()
        {
            if (window != null)
                window.Show();

            yield return null;
        }

        public override IEnumerator Undo()
        {
            yield return null;
        }
    }
}