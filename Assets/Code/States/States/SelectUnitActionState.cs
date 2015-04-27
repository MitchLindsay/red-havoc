using Assets.Code.Controllers;
using Assets.Code.Events;
using Assets.Code.Events.Events;
using Assets.Code.Graphs;
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
            StateTransition cancellingUnitMove = GetTransitionByID(TransitionID.Previous);
            if (cancellingUnitMove != null)
            {
                // 1. Disable Input
                DisableInputEvent disableInput = new DisableInputEvent(EventID.DisableInput, this, 
                    new EventArgs<InputHandler>(stateMachine.InputHandler));
                cancellingUnitMove.AddEvent(disableInput, CoroutineID.Execute);

                // 2. Move unit back to original location
                MoveUnitEvent moveUnit = new MoveUnitEvent(EventID.MoveUnit, this,
                    new EventArgs<Actors.Cursor, Pathfinder, float>(stateMachine.MouseCursor, stateMachine.Pathfinder, 0.1f));
                cancellingUnitMove.AddEvent(moveUnit, CoroutineID.Undo);

                // 3. Move camera to selected unit
                PanCameraToSelectedUnitObjectEvent panCameraToSelectedUnitObject = new PanCameraToSelectedUnitObjectEvent(
                    EventID.PanCameraToSelectedUnitObject, this, 
                    new EventArgs<CameraHandler, Actors.Cursor, float>(stateMachine.CameraHandler, stateMachine.MouseCursor, 1.0f));
                cancellingUnitMove.AddEvent(panCameraToSelectedUnitObject, CoroutineID.Execute);

                // 4. Enable Movement Area
                // 5. Enable Movement Line
                ShowMovementAreaEvent showMovementArea = new ShowMovementAreaEvent(
                    EventID.ShowMovementArea, this, 
                    new EventArgs<Pathfinder, Actors.Cursor>(stateMachine.Pathfinder, stateMachine.MouseCursor));
                cancellingUnitMove.AddEvent(showMovementArea, CoroutineID.Execute);

                // 6. Enable Input
                EnableInputEvent enableInput = new EnableInputEvent(EventID.EnableInput, this, 
                    new EventArgs<InputHandler>(stateMachine.InputHandler));
                cancellingUnitMove.AddEvent(enableInput, CoroutineID.Execute);
            }
        }

        public override void OnEntry()
        {
            base.OnEntry();

            InputHandler.OnBackButtonPress += ProceedToPreviousState;
        }

        public override void Update(float deltaTime) { }

        public override void OnExit()
        {
            InputHandler.OnBackButtonPress -= ProceedToPreviousState;

            base.OnExit();
        }

        private void ProceedToPreviousState()
        {
            RunEventsByTransitionID(TransitionID.Previous);
        }
    }
}