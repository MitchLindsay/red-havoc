using Assets.Code.Controllers;
using Assets.Code.Events;
using Assets.Code.Events.Events;
using Assets.Code.Graphs;
using Assets.Code.UI.Interactable;
using Assets.Code.UI.Static;
using UnityEngine;

namespace Assets.Code.States.States
{
    public class SelectUnitActionState : State
    {
        public SelectUnitActionState(StateID currentStateID) : base(currentStateID) { }

        public override void SetTransitions()
        {
            StateTransition cancellingUnitMove = new StateTransition(TransitionID.Previous, StateID.MoveUnit);
            AddTransition(cancellingUnitMove);

            StateTransition confirmingUnitAction = new StateTransition(TransitionID.Next, StateID.ConfirmUnitAction);
            AddTransition(confirmingUnitAction);
        }

        public override void SetTransitionEvents()
        {
            // Get Unit Action Menu
            GameObject unitActionMenuObject = GameObject.Find("Unit Action Menu");
            UnitActionMenu unitActionMenu = null;

            if (unitActionMenuObject != null)
                unitActionMenu = unitActionMenuObject.GetComponent<UnitActionMenu>();

            // Get Confirm Action Menu

            GameObject confirmActionMenuObject = GameObject.Find("Confirm Action Menu");
            ConfirmUnitActionMenu confirmActionMenu = null;

            if (confirmActionMenuObject != null)
                confirmActionMenu = confirmActionMenuObject.GetComponent<ConfirmUnitActionMenu>();

            StateTransition cancellingUnitMove = GetTransitionByID(TransitionID.Previous);
            if (cancellingUnitMove != null)
            {
                // 1. Disable Input
                DisableInputEvent disableInput = new DisableInputEvent(EventID.DisableInput, this,
                    new EventArgs<InputHandler>(stateMachine.InputHandler));
                cancellingUnitMove.AddEvent(disableInput, CoroutineID.Execute);

                // 2. Hide Unit Action Menu
                HideWindowEvent hideWindow = new HideWindowEvent(EventID.HideWindow, this, new EventArgs<Window>(unitActionMenu));
                cancellingUnitMove.AddEvent(hideWindow, CoroutineID.Execute);

                // 3. Move unit back to original location
                MoveUnitEvent moveUnit = new MoveUnitEvent(EventID.MoveUnit, this,
                    new EventArgs<Actors.Cursor, Pathfinder, float>(stateMachine.MouseCursor, stateMachine.Pathfinder, 0.1f));
                cancellingUnitMove.AddEvent(moveUnit, CoroutineID.Undo);

                // 4. Move camera to selected unit
                PanCameraToSelectedUnitObjectEvent panCameraToSelectedUnitObject = new PanCameraToSelectedUnitObjectEvent(
                    EventID.PanCameraToSelectedUnitObject, this,
                    new EventArgs<CameraHandler, Actors.Cursor, float>(stateMachine.CameraHandler, stateMachine.MouseCursor, 1.0f));
                cancellingUnitMove.AddEvent(panCameraToSelectedUnitObject, CoroutineID.Execute);

                // 5. Enable Movement Area
                // 6. Enable Movement Line
                ShowMovementAreaEvent showMovementArea = new ShowMovementAreaEvent(
                    EventID.ShowMovementArea, this,
                    new EventArgs<Pathfinder, Actors.Cursor>(stateMachine.Pathfinder, stateMachine.MouseCursor));
                cancellingUnitMove.AddEvent(showMovementArea, CoroutineID.Execute);

                // 7. Enable Input
                EnableInputEvent enableInput = new EnableInputEvent(EventID.EnableInput, this,
                    new EventArgs<InputHandler>(stateMachine.InputHandler));
                cancellingUnitMove.AddEvent(enableInput, CoroutineID.Execute);
            }

            StateTransition confirmingUnitAction = GetTransitionByID(TransitionID.Next);
            if (confirmingUnitAction != null)
            {
                // 1. Disable Input
                DisableInputEvent disableInput = new DisableInputEvent(EventID.DisableInput, this,
                    new EventArgs<InputHandler>(stateMachine.InputHandler));
                confirmingUnitAction.AddEvent(disableInput, CoroutineID.Execute);

                // 2. Hide Unit Action Menu
                HideWindowEvent hideWindow = new HideWindowEvent(EventID.HideWindow, this, new EventArgs<Window>(unitActionMenu));
                confirmingUnitAction.AddEvent(hideWindow, CoroutineID.Execute);

                // 3. Show Confirm Unit Action UI (only if capture/wait command)
                ShowWindowEvent showWindow = new ShowWindowEvent(EventID.ShowWindow, this, new EventArgs<Window>(confirmActionMenu));
                confirmingUnitAction.AddEvent(showWindow, CoroutineID.Execute);

                // 4. Enable Keyboard
                EnableKeyboardEvent enableKeyboard = new EnableKeyboardEvent(EventID.EnableKeyboard, this, new EventArgs<InputHandler>(stateMachine.InputHandler));
                confirmingUnitAction.AddEvent(enableKeyboard, CoroutineID.Execute);
            }
        }

        public override void OnEntry()
        {
            base.OnEntry();

            InputHandler.OnBackButtonPress += ProceedToPreviousState;
            UnitActionMenu.OnCancelClick += ProceedToPreviousState;
            UnitActionMenu.OnActionClick += ProceedToNextState;
        }

        public override void Update(float deltaTime) { }

        private void ProceedToPreviousState()
        {
            InputHandler.OnBackButtonPress -= ProceedToPreviousState;
            UnitActionMenu.OnCancelClick -= ProceedToPreviousState;
            UnitActionMenu.OnActionClick -= ProceedToNextState;

            RunEventsByTransitionID(TransitionID.Previous);
        }

        private void ProceedToNextState(EventID eventID)
        {
            InputHandler.OnBackButtonPress -= ProceedToPreviousState;
            UnitActionMenu.OnCancelClick -= ProceedToPreviousState;
            UnitActionMenu.OnActionClick -= ProceedToNextState;

            RunEventsByTransitionID(TransitionID.Next);
        }
    }
}
