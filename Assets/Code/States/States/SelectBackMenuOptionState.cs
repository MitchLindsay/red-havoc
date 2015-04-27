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

                CursorInfo cursorInfo = GameObject.Find("Cursor Info").GetComponent<CursorInfo>();
                ShowWindowEvent showCursorInfoEvent = new ShowWindowEvent(EventID.ShowCursorInfo, this, new EventArgs<Window>(cursorInfo));
                cancellingBackMenu.AddEvent(showCursorInfoEvent, CoroutineID.Execute);

                TurnInfo turnInfo = GameObject.Find("Turn Info").GetComponent<TurnInfo>();
                ShowWindowEvent showTurnInfoEvent = new ShowWindowEvent(EventID.ShowTurnInfo, this, new EventArgs<Window>(turnInfo));
                cancellingBackMenu.AddEvent(showTurnInfoEvent, CoroutineID.Execute);

                FactionInfo factionInfo = GameObject.Find("Faction Info").GetComponent<FactionInfo>();
                ShowWindowEvent showFactionInfoEvent = new ShowWindowEvent(EventID.ShowFactionInfo, this, new EventArgs<Window>(factionInfo));
                cancellingBackMenu.AddEvent(showFactionInfoEvent, CoroutineID.Execute);

                EnableCursorEvent enableCursor = new EnableCursorEvent(EventID.EnableCursor, this, 
                    new EventArgs<InputHandler>(stateMachine.InputHandler));
                cancellingBackMenu.AddEvent(enableCursor, CoroutineID.Execute);
            }

            StateTransition endingTurn = GetTransitionByID(TransitionID.Next);
            if (endingTurn != null)
            {
                HideWindowEvent hideWindow = new HideWindowEvent(EventID.HideWindow, this, new EventArgs<Window>(backMenu));
                endingTurn.AddEvent(hideWindow, CoroutineID.Execute);

                DisableInputEvent disableInputEvent = 
                    new DisableInputEvent(EventID.DisableInput, this, new EventArgs<InputHandler>(stateMachine.InputHandler));
                endingTurn.AddEvent(disableInputEvent, CoroutineID.Execute);
            }
        }

        public override void OnEntry()
        {
            base.OnEntry();

            BackMenu.OnEndTurnClick += ProceedToNextState;
            BackMenu.OnCancelClick += ProceedToPreviousState;
            InputHandler.OnBackButtonPress += ProceedToPreviousState;
        }

        public override void Update(float deltaTime) { }

        public override void OnExit()
        {
            BackMenu.OnEndTurnClick -= ProceedToNextState;
            BackMenu.OnCancelClick -= ProceedToPreviousState;
            InputHandler.OnBackButtonPress -= ProceedToPreviousState;

            base.OnExit();
        }

        private void ProceedToNextState()
        {
            RunEventsByTransitionID(TransitionID.Next);
        }

        private void ProceedToPreviousState()
        {
            RunEventsByTransitionID(TransitionID.Previous);
        }
    }
}