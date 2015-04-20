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
        private CameraHandler cameraHandler;

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
            // Get Controllers
            inputHandler = InputHandler.Instance;
            cameraHandler = CameraHandler.Instance;

            StateTransition selectingBackMenuOption = GetTransitionByID(TransitionID.Previous);
            if (selectingBackMenuOption != null)
            {
                GameObject backMenuObject = GameObject.Find("Back Menu");
                BackMenu backMenu = null;

                if (backMenuObject != null)
                    backMenu = backMenuObject.GetComponent<BackMenu>();

                ShowWindowEvent showWindow = new ShowWindowEvent(EventID.ShowWindow, this, new EventArgs<Window>(backMenu));
                selectingBackMenuOption.AddEvent(showWindow, CoroutineID.Execute);

                DisableInputEvent disableInput = new DisableInputEvent(EventID.DisableInput, this, new EventArgs<InputHandler>(inputHandler));
                selectingBackMenuOption.AddEvent(disableInput, CoroutineID.Execute);
            }

            StateTransition movingUnit = GetTransitionByID(TransitionID.Next);
            if (movingUnit != null)
            {
                // 1. Disable Input
                DisableInputEvent disableInput = new DisableInputEvent(EventID.DisableInput, this, new EventArgs<InputHandler>(inputHandler));
                movingUnit.AddEvent(disableInput, CoroutineID.Execute);

                // 2. Get Selected Unit
                // 3. Pan Camera to Selected Unit
                PanCameraToGameObjectEvent panCameraToGameObjectEvent = new PanCameraToGameObjectEvent(
                    EventID.PanCameraToGameObject, this, new EventArgs<CameraHandler, GameObject, float>(cameraHandler, null, 1.0f));
                movingUnit.AddEvent(panCameraToGameObjectEvent, CoroutineID.Execute);

                // Show Movement Area of Selected Unit
                // Show Movement Line within Movement Area
                // Show Expanded Unit GUI
                // Enable Input
            }
        }

        public override void OnEntry()
        {
            base.OnEntry();

            InputHandler.OnBackButtonPress += ProceedToPreviousState;
            Actors.Cursor.OnMouseClickUnit += ProceedToNextState;
        }

        public override void Update(float deltaTime) { }

        public override void OnExit()
        {
            InputHandler.OnBackButtonPress -= ProceedToPreviousState;
            Actors.Cursor.OnMouseClickUnit -= ProceedToNextState;

            base.OnExit();
        }

        private void ProceedToPreviousState()
        {
            RunEventsByTransitionID(TransitionID.Previous);
        }

        private void ProceedToNextState(GameObject collidedObject)
        {
            // SOMEHOW SET SELECTED UNIT?
            
            RunEventsByTransitionID(TransitionID.Next);
        }
    }
}