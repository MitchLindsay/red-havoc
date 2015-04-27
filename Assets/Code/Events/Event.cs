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
        HideUnitWindow,
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
        ShowTurnInfo,
        HideTurnInfo,
        ShowFactionInfo,
        HideFactionInfo,
        ShowCursorInfo,
        HideCursorInfo,
        SelectUnit,
        DeselectUnit,
        MoveUnit,
        EnableCursor,
        DisableCursor,
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
        public Event(EventID eventID, object sender, EventArgs e)
        {
            this.EventID = eventID;
        }

        public abstract IEnumerator Execute();
        public virtual IEnumerator Undo()
        {
            yield return null;
        }
    }
}