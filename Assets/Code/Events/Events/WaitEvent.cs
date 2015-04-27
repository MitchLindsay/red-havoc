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
                yield return new WaitForSeconds(1.0f);
        }
    }
}