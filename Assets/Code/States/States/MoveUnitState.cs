using Assets.Code.Actors;
using Assets.Code.Controllers;
using Assets.Code.Events;
using Assets.Code.Events.Events;
using Assets.Code.Graphs;
using Assets.Code.UI.Static;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.States.States
{
    public class MoveUnitState : State
    {
        public MoveUnitState(StateID currentStateID) : base(currentStateID) { }

        public override void SetTransitions()
        {
            StateTransition deselectingUnit = new StateTransition(TransitionID.Previous, StateID.SelectUnit);
            AddTransition(deselectingUnit);

            StateTransition confirmingUnitMove = new StateTransition(TransitionID.Next, StateID.SelectUnitAction);
            AddTransition(confirmingUnitMove);
        }

        public override void SetTransitionEvents()
        {
            StateTransition deselectingUnit = GetTransitionByID(TransitionID.Previous);
            if (deselectingUnit != null)
            {   
                // 1. Disable Input
                DisableInputEvent disableInput = new DisableInputEvent(EventID.DisableInput, this, 
                    new EventArgs<InputHandler>(stateMachine.InputHandler));
                deselectingUnit.AddEvent(disableInput, CoroutineID.Execute);

                // 2. Hide Expanded Unit GUI
                UnitWindow unitWindow = GameObject.Find("Unit Window").GetComponent<UnitWindow>();
                HideWindowEvent hideUnitWindow = new HideWindowEvent(EventID.HideUnitWindow, this, new EventArgs<Window>(unitWindow));
                deselectingUnit.AddEvent(hideUnitWindow, CoroutineID.Execute);

                // 3. Hide Movement Area of Selected Unit
                // 4. Hide Movement Line within Movement Area
                HideMovementAreaEvent hideMovementArea = new HideMovementAreaEvent(EventID.HideMovementArea, this, 
                    new EventArgs<Pathfinder>(stateMachine.Pathfinder));
                deselectingUnit.AddEvent(hideMovementArea, CoroutineID.Execute);

                // 5. Deselect Unit
                DeselectUnitEvent deselectUnit = new DeselectUnitEvent(EventID.DeselectUnit, this,
                    new EventArgs<Actors.Cursor>(stateMachine.MouseCursor));
                deselectingUnit.AddEvent(deselectUnit, CoroutineID.Execute);

                // 6. Enable Input
                EnableInputEvent enableInput = new EnableInputEvent(EventID.EnableInput, this, 
                    new EventArgs<InputHandler>(stateMachine.InputHandler));
                deselectingUnit.AddEvent(enableInput, CoroutineID.Execute);
            }

            StateTransition confirmingUnitMove = GetTransitionByID(TransitionID.Next);
            if (confirmingUnitMove != null)
            {
                // 1. Disable Input
                DisableInputEvent disableInput = new DisableInputEvent(EventID.DisableInput, this, 
                    new EventArgs<InputHandler>(stateMachine.InputHandler));
                confirmingUnitMove.AddEvent(disableInput, CoroutineID.Execute);

                // 2. Disable Movement Area
                // 3. Disable Movement Line
                HideMovementAreaEvent hideMovementArea = new HideMovementAreaEvent(
                    EventID.HideMovementArea, this, new EventArgs<Pathfinder>(stateMachine.Pathfinder));
                confirmingUnitMove.AddEvent(hideMovementArea, CoroutineID.Execute);

                // 4. Move unit to selected node
                MoveUnitEvent moveUnit = new MoveUnitEvent(EventID.MoveUnit, this, 
                    new EventArgs<Actors.Cursor, Pathfinder, float>(stateMachine.MouseCursor, stateMachine.Pathfinder, 0.1f));
                confirmingUnitMove.AddEvent(moveUnit, CoroutineID.Execute);

                // 5. Pan Camera to Selected Unit
                PanCameraToSelectedUnitObjectEvent panCameraToSelectedUnitObject = new PanCameraToSelectedUnitObjectEvent(
                    EventID.PanCameraToSelectedUnitObject, this, 
                    new EventArgs<CameraHandler, Actors.Cursor, float>(stateMachine.CameraHandler, stateMachine.MouseCursor, 1.0f));
                confirmingUnitMove.AddEvent(panCameraToSelectedUnitObject, CoroutineID.Execute);

                // 6. Enable input
                EnableInputEvent enableInput = new EnableInputEvent(EventID.EnableInput, this, 
                    new EventArgs<InputHandler>(stateMachine.InputHandler));
                confirmingUnitMove.AddEvent(enableInput, CoroutineID.Execute);
            }
        }

        public override void OnEntry()
        {
            base.OnEntry();

            InputHandler.OnBackButtonPress += ProceedToPreviousState;
            Actors.Cursor.OnMouseClickNode += ProceedToNextState;
        }

        public override void Update(float deltaTime) { }

        public override void OnExit()
        {
            base.OnExit();
        }

        private void ProceedToPreviousState()
        {
            RunEventsByTransitionID(TransitionID.Previous);
        }

        private void ProceedToNextState(GameObject collidedObject)
        {
            if (collidedObject != null)
            {
                PathfindingNode targetNode = collidedObject.GetComponent<PathfindingNode>();
                if (targetNode != null)
                {
                    HashSet<PathfindingNode> area = stateMachine.Pathfinder.LastAreaGenerated;
                    if (area.Contains(targetNode) && stateMachine.MouseCursor.SelectedUnit.IsActive)
                    {
                        InputHandler.OnBackButtonPress -= ProceedToPreviousState;
                        Actors.Cursor.OnMouseClickNode -= ProceedToNextState;

                        RunEventsByTransitionID(TransitionID.Next);
                    }
                }
            }
        }
    }
}