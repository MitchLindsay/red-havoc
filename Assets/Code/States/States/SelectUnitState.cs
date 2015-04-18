using Assets.Code.Actors;
using Assets.Code.Controllers;
using Assets.Code.Events;
using Assets.Code.Events.Events;
using Assets.Code.UI.Interactable;
using Assets.Code.UI.Static;
using UnityEngine;

namespace Assets.Code.States.States
{
    public class SelectUnitState : State
    {
        private InputHandler inputHandler;

        public SelectUnitState(StateID currentStateID) : base(currentStateID) { }

        public override void SetTransitions()
        {
            StateTransition selectingBackMenuOption = new StateTransition(TransitionID.Previous, StateID.SelectBackMenuOption);
            AddTransition(selectingBackMenuOption);

            StateTransition movingUnit = new StateTransition(TransitionID.Next, StateID.MoveUnit);
            AddTransition(movingUnit);
        }

        public override void SetTransitionEvents()
        {
            StateTransition selectingBackMenuOption = GetTransitionByID(TransitionID.Previous);
            if (selectingBackMenuOption != null)
            {
                // Get Controllers
                inputHandler = InputHandler.Instance;

                GameObject backMenuObject = GameObject.Find("Back Menu");
                BackMenu backMenu = null;

                if (backMenuObject != null)
                    backMenu = backMenuObject.GetComponent<BackMenu>();

                ShowWindowEvent showWindow = new ShowWindowEvent(EventID.ShowWindow, this, new EventArgs<Window>(backMenu));
                selectingBackMenuOption.AddEvent(showWindow, CoroutineID.Execute);

                DisableInputEvent disableInput = new DisableInputEvent(EventID.DisableInput, this, new EventArgs<InputHandler>(inputHandler));
                selectingBackMenuOption.AddEvent(disableInput, CoroutineID.Execute);
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

        public void ProceedToPreviousState()
        {
            RunEventsByTransitionID(TransitionID.Previous);
        }
    }
}