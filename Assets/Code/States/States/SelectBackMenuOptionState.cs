using Assets.Code.Controllers;
using Assets.Code.Events;
using Assets.Code.Events.Events;
using Assets.Code.UI.Interactable;
using Assets.Code.UI.Static;
using UnityEngine;

namespace Assets.Code.States.States
{
    public class SelectBackMenuOptionState : State
    {
        private InputHandler inputHandler;

        public SelectBackMenuOptionState(StateID currentStateID) : base(currentStateID) { }

        public override void SetTransitions()
        {
            StateTransition cancellingBackMenu = new StateTransition(TransitionID.Previous, StateID.SelectUnit);
            AddTransition(cancellingBackMenu);

            StateTransition endingTurn = new StateTransition(TransitionID.Next, StateID.ChangeTurns);
            AddTransition(endingTurn);
        }

        public override void SetTransitionEvents()
        {
            // Get Controllers
            inputHandler = InputHandler.Instance;

            // Get Back Menu
            GameObject backMenuObject = GameObject.Find("Back Menu");
            BackMenu backMenu = null;

            if (backMenuObject != null)
                backMenu = backMenuObject.GetComponent<BackMenu>();


            StateTransition cancellingBackMenu = GetTransitionByID(TransitionID.Previous);
            if (cancellingBackMenu != null)
            {
                HideWindowEvent hideWindow = new HideWindowEvent(EventID.HideWindow, this, new EventArgs<Window>(backMenu));
                cancellingBackMenu.AddEvent(hideWindow, CoroutineID.Execute);

                EnableInputEvent enableInput = new EnableInputEvent(EventID.EnableInput, this, new EventArgs<InputHandler>(inputHandler));
                cancellingBackMenu.AddEvent(enableInput, CoroutineID.Execute);
            }

            StateTransition endingTurn = GetTransitionByID(TransitionID.Next);
            if (endingTurn != null)
            {
                HideWindowEvent hideWindow = new HideWindowEvent(EventID.HideWindow, this, new EventArgs<Window>(backMenu));
                endingTurn.AddEvent(hideWindow, CoroutineID.Execute);

                DisableInputEvent disableInputEvent = new DisableInputEvent(EventID.DisableInput, this, new EventArgs<InputHandler>(inputHandler));
                endingTurn.AddEvent(disableInputEvent, CoroutineID.Execute);
            }
        }

        public override void OnEntry()
        {
            base.OnEntry();

            BackMenu.OnEndTurnClick += ProceedToNextTurn;
            BackMenu.OnCancelClick += ProceedToPreviousTurn;
        }

        public override void Update(float deltaTime) { }

        public override void OnExit()
        {
            BackMenu.OnEndTurnClick -= ProceedToNextTurn;
            BackMenu.OnCancelClick -= ProceedToPreviousTurn;

            base.OnExit();
        }

        private void ProceedToNextTurn()
        {
            RunEventsByTransitionID(TransitionID.Next);
        }

        private void ProceedToPreviousTurn()
        {
            RunEventsByTransitionID(TransitionID.Previous);
        }
    }
}