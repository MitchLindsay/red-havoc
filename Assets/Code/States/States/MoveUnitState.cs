using Assets.Code.Controllers;
using Assets.Code.Events;
using Assets.Code.Events.Events;
using Assets.Code.Graphs;
using Assets.Code.UI.Static;
using UnityEngine;

namespace Assets.Code.States.States
{
    public class MoveUnitState : State
    {
        private InputHandler inputHandler;
        private Pathfinder pathfinder;

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
            // Get Controllers
            inputHandler = InputHandler.Instance;
            pathfinder = GameObject.Find("Pathfinder").GetComponent<Pathfinder>();

            StateTransition deselectingUnit = GetTransitionByID(TransitionID.Previous);
            if (deselectingUnit != null)
            {   
                // 1. Disable Input
                DisableInputEvent disableInput = new DisableInputEvent(EventID.DisableInput, this, new EventArgs<InputHandler>(inputHandler));
                deselectingUnit.AddEvent(disableInput, CoroutineID.Execute);

                // 2. Hide Expanded Unit GUI
                UnitWindow unitWindow = GameObject.Find("Unit Window").GetComponent<UnitWindow>();
                HideWindowEvent hideUnitWindow = new HideWindowEvent(EventID.HideUnitWindow, this, new EventArgs<Window>(unitWindow));
                deselectingUnit.AddEvent(hideUnitWindow, CoroutineID.Execute);

                // 3. Hide Movement Area of Selected Unit
                // 4. Hide Movement Line within Movement Area
                HideMovementAreaEvent hideMovementArea = new HideMovementAreaEvent(EventID.HideMovementArea, this, new EventArgs<Pathfinder>(pathfinder));
                deselectingUnit.AddEvent(hideMovementArea, CoroutineID.Execute);

                // 5. Enable Input
                EnableInputEvent enableInput = new EnableInputEvent(EventID.EnableInput, this, new EventArgs<InputHandler>(inputHandler));
                deselectingUnit.AddEvent(enableInput, CoroutineID.Execute);
            }

        }

        public override void OnEntry()
        {
            base.OnEntry();

            InputHandler.OnBackButtonPress += ProceedToPreviousState;
        }

        public override void Update(float deltaTime)
        {
        }

        public override void OnExit()
        {
            InputHandler.OnBackButtonPress -= ProceedToPreviousState;

            base.OnExit();
        }

        private void ProceedToPreviousState()
        {
            RunEventsByTransitionID(TransitionID.Previous);
        }

        /*
        private void ProceedToNextState(GameObject collidedObject)
        {
            if (collidedObject != null)
            {
                if (collidedObject.GetComponent<Unit>() != null)
                    RunEventsByTransitionID(TransitionID.Next);
            }
        }
        */
    }
}