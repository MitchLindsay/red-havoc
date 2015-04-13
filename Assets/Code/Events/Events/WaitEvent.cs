using System.Collections;
using UnityEngine;

namespace Assets.Code.Events.Events
{
    public class WaitEvent : Event
    {
        private float waitTime;

        public WaitEvent(EventID eventID, object sender, EventArgs<float> e) : base(eventID, sender, e)
        {
            this.waitTime = e.Value;
        }

        public override IEnumerator Execute()
        {
            for (float i = waitTime; i > 0; i--)
            {
                Debug.Log("Waiting for " + i + " seconds.");
                yield return new WaitForSeconds(1.0f);
            }
        }

        public override IEnumerator Undo()
        {
            yield return null;
        }
    }
}