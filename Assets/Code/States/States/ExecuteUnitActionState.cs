using Assets.Code.Actors;
using Assets.Code.Controllers;
using Assets.Code.Events;
using Assets.Code.Events.Events;
using Assets.Code.UI.Static;
using UnityEngine;

namespace Assets.Code.States.States
{
    public class ExecuteUnitActionState : State
    {
        public ExecuteUnitActionState(StateID currentStateID) : base(currentStateID) { }

        public override void SetTransitions()
        {
            StateTransition selectingUnit = new StateTransition(TransitionID.Previous, StateID.SelectUnit);
            AddTransition(selectingUnit);

            StateTransition changingTurns = new StateTransition(TransitionID.Next, StateID.ChangeTurns);
            AddTransition(changingTurns);
        }

        public override void SetTransitionEvents()
        {
            StateTransition selectingUnit = GetTransitionByID(TransitionID.Previous);
            if (selectingUnit != null)
            {
                // 1. Disable Input
                DisableInputEvent disableInputEvent = new DisableInputEvent(EventID.DisableInput, this, new EventArgs<InputHandler>(stateMachine.InputHandler));
                selectingUnit.AddEvent(disableInputEvent, CoroutineID.Execute);

                // 2. Hide Expanded Unit GUI
                UnitWindow unitWindow = GameObject.Find("Unit Window").GetComponent<UnitWindow>();
                HideWindowEvent hideUnitWindow = new HideWindowEvent(EventID.HideUnitWindow, this, new EventArgs<Window>(unitWindow));
                selectingUnit.AddEvent(hideUnitWindow, CoroutineID.Execute);

                // 3. Deactivate Unit
                DeactivateUnitEvent deactivateUnitEvent = new DeactivateUnitEvent
                    (EventID.DeactivateUnit, this, new EventArgs<Actors.Cursor, Faction>(stateMachine.MouseCursor, stateMachine.TurnHandler.ActiveFaction));
                selectingUnit.AddEvent(deactivateUnitEvent, CoroutineID.Execute);

                // 4. Deselect Unit
                DeselectUnitEvent deselectUnit = new DeselectUnitEvent(EventID.DeselectUnit, this,
                    new EventArgs<Actors.Cursor>(stateMachine.MouseCursor));
                selectingUnit.AddEvent(deselectUnit, CoroutineID.Execute);

                // 5. Enable Input
                EnableInputEvent enableInput = new EnableInputEvent(EventID.EnableInput, this,
                    new EventArgs<InputHandler>(stateMachine.InputHandler));
                selectingUnit.AddEvent(enableInput, CoroutineID.Execute);
            }

            StateTransition changingTurns = GetTransitionByID(TransitionID.Next);
            if (changingTurns != null)
            {
                // 1. Disable Input
                DisableInputEvent disableInputEvent = new DisableInputEvent(EventID.DisableInput, this, new EventArgs<InputHandler>(stateMachine.InputHandler));
                changingTurns.AddEvent(disableInputEvent, CoroutineID.Execute);

                // 2. Hide Expanded Unit GUI
                UnitWindow unitWindow = GameObject.Find("Unit Window").GetComponent<UnitWindow>();
                HideWindowEvent hideUnitWindow = new HideWindowEvent(EventID.HideUnitWindow, this, new EventArgs<Window>(unitWindow));
                changingTurns.AddEvent(hideUnitWindow, CoroutineID.Execute);

                // 3. Deactivate Unit
                DeactivateUnitEvent deactivateUnitEvent = new DeactivateUnitEvent
                    (EventID.DeactivateUnit, this, new EventArgs<Actors.Cursor, Faction>(stateMachine.MouseCursor, stateMachine.TurnHandler.ActiveFaction));
                changingTurns.AddEvent(deactivateUnitEvent, CoroutineID.Execute);

                // 4. Deselect Unit
                DeselectUnitEvent deselectUnit = new DeselectUnitEvent(EventID.DeselectUnit, this,
                    new EventArgs<Actors.Cursor>(stateMachine.MouseCursor));
                changingTurns.AddEvent(deselectUnit, CoroutineID.Execute);
            }
        }

        public override void OnEntry()
        {
            base.OnEntry();

            if (stateMachine.TurnHandler.ActiveFaction.ActiveUnitCount() > 1)
                RunEventsByTransitionID(TransitionID.Previous);
            else
                RunEventsByTransitionID(TransitionID.Next);
        }

        public override void Update(float deltaTime) { }
    }
}
