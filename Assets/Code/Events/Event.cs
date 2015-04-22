using System;
using System.Collections;

namespace Assets.Code.Events
{
    public enum EventID
    {
        Null,
        Wait,
        ShowSplashScreen,
        ShowTurnIndicator,
        ShowUnitWindow,
        ChangeActiveFaction,
        PanCameraToPosition,
        PanCameraToGameObject,
        PanCameraToNearestActiveUnit,
        PanCameraToSelectedUnitObject,
        EnableInput,
        DisableInput,
        ShowWindow,
        HideWindow,
        ShowMovementArea,
        HideMovementArea,
    }

    public enum CoroutineID
    {
        Null,
        Execute,
        Undo
    }

    public abstract class Event
    {
        public EventID EventID;
        public Event(EventID eventID, object sender, EventArgs e) { this.EventID = eventID;  }
        public abstract IEnumerator Execute();
        public abstract IEnumerator Undo();
    }
}