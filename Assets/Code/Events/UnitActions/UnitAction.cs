using System;

namespace Assets.Code.Events.UnitActions
{
    public abstract class UnitAction : Event
    {
        public UnitAction(EventID eventID, object sender, EventArgs e) : base(eventID, sender, e) { }
    }
}