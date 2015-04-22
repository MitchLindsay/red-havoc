using Assets.Code.Actors;
using Assets.Code.UI.Interactable;
using Assets.Code.UI.Static;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.Events
{
    public class ShowUnitWindowEvent : Event
    {
        private UnitWindow window;
        private GameObject unitObject;
        private Actors.Cursor cursor;

        public ShowUnitWindowEvent(EventID eventID, object sender, EventArgs<UnitWindow, Actors.Cursor> e) : base(eventID, sender, e)
        {
            this.window = e.Value;
            this.cursor = e.Value2;
        }

        public override IEnumerator Execute()
        {
            if (window != null)
            {
                Debug.Log("Displaying Window: " + window);

                if (unitObject == null)
                    unitObject = cursor.LastClickedUnitObject;

                window.SetUnitInfo(unitObject);
            }
            else
                Debug.Log("Window is null!");

            yield return null;
        }

        public override IEnumerator Undo()
        {
            window.Hide();
            yield return null;
        }
    }
}