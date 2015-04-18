using Assets.Code.Actors;
using Assets.Code.UI.Interactable;
using Assets.Code.UI.Static;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.Events
{
    public class HideWindowEvent : Event
    {
        private Window window;

        public HideWindowEvent(EventID eventID, object sender, EventArgs<Window> e) : base(eventID, sender, e)
        {
            this.window = e.Value;
        }

        public override IEnumerator Execute()
        {
            if (window != null)
            {
                Debug.Log("Hiding Window: " + window);
                window.Hide();
            }
            else
                Debug.Log("Window is null!");

            yield return null;
        }

        public override IEnumerator Undo()
        {
            yield return null;
        }
    }
}