using Assets.Code.Controllers;
using Assets.Code.Events;
using Assets.Code.Events.Events;
using Assets.Code.UI;
using Assets.Code.UI.Static;
using UnityEngine;

namespace Assets.Code.States.States
{
    public class ChangeTurnState : State
    {
        public ChangeTurnState(StateID currentStateID) : base(currentStateID) { }

        public override void SetTransitions()
        {
            StateTransition selectingUnit = new StateTransition(TransitionID.Next, StateID.SelectUnit);
            AddTransition(selectingUnit);
        }

        public override void SetTransitionEvents()
        {
            StateTransition selectingUnit = GetTransitionByID(TransitionID.Next);
            if (selectingUnit != null)
            {
                DisableInputEvent disableInputEvent = new DisableInputEvent(EventID.DisableInput, this, new EventArgs<InputHandler>(stateMachine.InputHandler));
                selectingUnit.AddEvent(disableInputEvent, CoroutineID.Execute);

                // 1. Change active factions
                // 2. Change active units
                // 3. Increment turn counter
                ChangeActiveFactionEvent changeActiveFactionEvent = new ChangeActiveFactionEvent
                    (EventID.ChangeActiveFaction, this, new EventArgs<TurnHandler>(stateMachine.TurnHandler));
                selectingUnit.AddEvent(changeActiveFactionEvent, CoroutineID.Execute);

                // 4. Display turn GUI
                TurnIndicator turnIndicator = GameObject.Find("Turn Indicator").GetComponent<TurnIndicator>();
                ShowTurnIndicatorEvent showTurnIndicatorEvent = new ShowTurnIndicatorEvent
                    (EventID.ShowTurnIndicator, this, new EventArgs<TurnIndicator, float>(turnIndicator, 3.0f));
                selectingUnit.AddEvent(showTurnIndicatorEvent, CoroutineID.Execute);

                // 5. Pan to nearest active unit
                GameObject nearestActiveUnitObject = null;
                PanCameraToGameObjectEvent panCameraToGameObjectEvent = new PanCameraToGameObjectEvent
                    (EventID.PanCameraToNearestActiveUnit, this,
                    new EventArgs<CameraHandler, GameObject, float>(stateMachine.CameraHandler, nearestActiveUnitObject, 1.0f));
                selectingUnit.AddEvent(panCameraToGameObjectEvent, CoroutineID.Execute);

                // 6. Show Cursor Info
                CursorInfo cursorInfo = GameObject.Find("Cursor Info").GetComponent<CursorInfo>();
                ShowWindowEvent showCursorInfoEvent = new ShowWindowEvent(EventID.ShowCursorInfo, this, new EventArgs<Window>(cursorInfo));
                selectingUnit.AddEvent(showCursorInfoEvent, CoroutineID.Execute);

                // 7. Show Turn Info
                TurnInfo turnInfo = GameObject.Find("Turn Info").GetComponent<TurnInfo>();
                ShowWindowEvent showTurnInfoEvent = new ShowWindowEvent(EventID.ShowTurnInfo, this, new EventArgs<Window>(turnInfo));
                selectingUnit.AddEvent(showTurnInfoEvent, CoroutineID.Execute);

                // 8. Show Faction Info
                FactionInfo factionInfo = GameObject.Find("Faction Info").GetComponent<FactionInfo>();
                ShowWindowEvent showFactionInfoEvent = new ShowWindowEvent(EventID.ShowFactionInfo, this, new EventArgs<Window>(factionInfo));
                selectingUnit.AddEvent(showFactionInfoEvent, CoroutineID.Execute);

                // 9. Enable Input
                // 10. Enable Camera Drag
                EnableInputEvent enableInputEvent = new EnableInputEvent(EventID.EnableInput, this, new EventArgs<InputHandler>(stateMachine.InputHandler));
                selectingUnit.AddEvent(enableInputEvent, CoroutineID.Execute);
            }
        }

        public override void OnEntry()
        {
            base.OnEntry();
            RunEventsByTransitionID(TransitionID.Next);
        }

        public override void Update(float deltaTime) { }
    }
}
