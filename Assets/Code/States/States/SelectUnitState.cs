using Assets.Code.Actors;
using Assets.Code.Controllers;
using Assets.Code.Events;
using Assets.Code.Events.Events;
using Assets.Code.Graphs;
using Assets.Code.UI.Interactable;
using Assets.Code.UI.Static;
using UnityEngine;

namespace Assets.Code.States.States
{
    public class SelectUnitState : State
    {
        private InputHandler inputHandler;
        private CameraHandler cameraHandler;
        private Pathfinder pathfinder;
        private Actors.Cursor cursor;

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
            pathfinder = GameObject.Find("Pathfinder").GetComponent<Pathfinder>();
            cursor = GameObject.Find("Mouse Cursor").GetComponent<Actors.Cursor>();

            StateTransition selectingBackMenuOption = GetTransitionByID(TransitionID.Previous);
            if (selectingBackMenuOption != null)
            {
                DisableInputEvent disableInput = new DisableInputEvent(EventID.DisableInput, this, new EventArgs<InputHandler>(inputHandler));
                selectingBackMenuOption.AddEvent(disableInput, CoroutineID.Execute);

                CursorInfo cursorInfo = GameObject.Find("Cursor Info").GetComponent<CursorInfo>();
                HideWindowEvent hideCursorInfoEvent = new HideWindowEvent(EventID.HideCursorInfo, this, new EventArgs<Window>(cursorInfo));
                selectingBackMenuOption.AddEvent(hideCursorInfoEvent, CoroutineID.Execute);

                TurnInfo turnInfo = GameObject.Find("Turn Info").GetComponent<TurnInfo>();
                HideWindowEvent hideTurnInfoEvent = new HideWindowEvent(EventID.HideTurnInfo, this, new EventArgs<Window>(turnInfo));
                selectingBackMenuOption.AddEvent(hideTurnInfoEvent, CoroutineID.Execute);

                FactionInfo factionInfo = GameObject.Find("Faction Info").GetComponent<FactionInfo>();
                HideWindowEvent hideFactionInfoEvent = new HideWindowEvent(EventID.HideFactionInfo, this, new EventArgs<Window>(factionInfo));
                selectingBackMenuOption.AddEvent(hideFactionInfoEvent, CoroutineID.Execute);

                GameObject backMenuObject = GameObject.Find("Back Menu");
                BackMenu backMenu = null;

                if (backMenuObject != null)
                    backMenu = backMenuObject.GetComponent<BackMenu>();

                ShowWindowEvent showWindow = new ShowWindowEvent(EventID.ShowWindow, this, new EventArgs<Window>(backMenu));
                selectingBackMenuOption.AddEvent(showWindow, CoroutineID.Execute);
            }

            StateTransition movingUnit = GetTransitionByID(TransitionID.Next);
            if (movingUnit != null)
            {
                // 1. Disable Input
                DisableInputEvent disableInput = new DisableInputEvent(EventID.DisableInput, this, new EventArgs<InputHandler>(inputHandler));
                movingUnit.AddEvent(disableInput, CoroutineID.Execute);

                // 2. Show Expanded Unit GUI
                UnitWindow unitWindow = GameObject.Find("Unit Window").GetComponent<UnitWindow>();
                ShowUnitWindowEvent showUnitWindow = new ShowUnitWindowEvent
                    (EventID.ShowUnitWindow, this, new EventArgs<UnitWindow, Actors.Cursor>(unitWindow, cursor));
                movingUnit.AddEvent(showUnitWindow, CoroutineID.Execute);

                // 3. Pan Camera to Selected Unit
                PanCameraToSelectedUnitObjectEvent panCameraToSelectedUnitObject = new PanCameraToSelectedUnitObjectEvent(
                    EventID.PanCameraToSelectedUnitObject, this, new EventArgs<CameraHandler, Actors.Cursor, float>(cameraHandler, cursor, 1.0f));
                movingUnit.AddEvent(panCameraToSelectedUnitObject, CoroutineID.Execute);

                // 4. Show Movement Area of Selected Unit
                // 5. Show Movement Line within Movement Area
                ShowMovementAreaEvent showMovementArea = new ShowMovementAreaEvent(
                    EventID.ShowMovementArea, this, new EventArgs<Pathfinder, Actors.Cursor>(pathfinder, cursor));
                movingUnit.AddEvent(showMovementArea, CoroutineID.Execute);

                // 6. Enable Input
                EnableInputEvent enableInput = new EnableInputEvent(EventID.EnableInput, this, new EventArgs<InputHandler>(inputHandler));
                movingUnit.AddEvent(enableInput, CoroutineID.Execute);
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
            if (collidedObject != null)
            {
                if (collidedObject.GetComponent<Unit>() != null)
                    RunEventsByTransitionID(TransitionID.Next);
            }
        }
    }
}