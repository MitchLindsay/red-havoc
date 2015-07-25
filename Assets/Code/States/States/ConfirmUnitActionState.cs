using Assets.Code.Controllers;
using Assets.Code.Events;
using Assets.Code.Events.Events;
using Assets.Code.UI.Interactable;
using Assets.Code.UI.Static;
using UnityEngine;

namespace Assets.Code.States.States
{
    public class ConfirmUnitActionState : State
    {
        public ConfirmUnitActionState(StateID currentStateID) : base(currentStateID) { }

        public override void SetTransitions()
        {
            StateTransition cancellingUnitAction = new StateTransition(TransitionID.Previous, StateID.SelectUnitAction);
            AddTransition(cancellingUnitAction);

            StateTransition executingUnitAction = new StateTransition(TransitionID.Next, StateID.ExecuteUnitAction);
            AddTransition(executingUnitAction);
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

            StateTransition cancellingUnitAction = GetTransitionByID(TransitionID.Previous);
            if (cancellingUnitAction != null)
            {
                // 1. Disable Input
                DisableInputEvent disableInput = new DisableInputEvent(EventID.DisableInput, this,
                    new EventArgs<InputHandler>(stateMachine.InputHandler));
                cancellingUnitAction.AddEvent(disableInput, CoroutineID.Execute);

                // 2. Hide Confirm Unit Action UI
                HideWindowEvent hideWindow = new HideWindowEvent(EventID.HideWindow, this, new EventArgs<Window>(confirmActionMenu));
                cancellingUnitAction.AddEvent(hideWindow, CoroutineID.Execute);

                // 3. Show Unit Action Menu
                ShowWindowEvent showWindow = new ShowWindowEvent(EventID.ShowWindow, this, new EventArgs<Window>(unitActionMenu));
                cancellingUnitAction.AddEvent(showWindow, CoroutineID.Execute);

                // 4. Enable Keyboard
                EnableKeyboardEvent enableKeyboard = new EnableKeyboardEvent(EventID.EnableKeyboard, this, new EventArgs<InputHandler>(stateMachine.InputHandler));
                cancellingUnitAction.AddEvent(enableKeyboard, CoroutineID.Execute);
            }

            StateTransition executingUnitAction = GetTransitionByID(TransitionID.Next);
            if (executingUnitAction != null)
            {
                // 1. Disable Input
                DisableInputEvent disableInput = new DisableInputEvent(EventID.DisableInput, this,
                    new EventArgs<InputHandler>(stateMachine.InputHandler));
                executingUnitAction.AddEvent(disableInput, CoroutineID.Execute);

                // 2. Hide Confirm Unit Action UI
                HideWindowEvent hideWindow = new HideWindowEvent(EventID.HideWindow, this, new EventArgs<Window>(confirmActionMenu));
                executingUnitAction.AddEvent(hideWindow, CoroutineID.Execute);
            }
        }

        public override void OnEntry()
        {
            base.OnEntry();

            InputHandler.OnBackButtonPress += ProceedToPreviousState;
            ConfirmUnitActionMenu.OnNoClick += ProceedToPreviousState;
            ConfirmUnitActionMenu.OnYesClick += ProceedToNextState;
        }

        public override void Update(float deltaTime) { }

        private void ProceedToPreviousState()
        {
            InputHandler.OnBackButtonPress -= ProceedToPreviousState;
            ConfirmUnitActionMenu.OnNoClick -= ProceedToPreviousState;
            ConfirmUnitActionMenu.OnYesClick -= ProceedToNextState;

            RunEventsByTransitionID(TransitionID.Previous);
        }

        private void ProceedToNextState()
        {
            InputHandler.OnBackButtonPress -= ProceedToPreviousState;
            ConfirmUnitActionMenu.OnNoClick -= ProceedToPreviousState;
            ConfirmUnitActionMenu.OnYesClick -= ProceedToNextState;

            RunEventsByTransitionID(TransitionID.Next);
        }
    }
}