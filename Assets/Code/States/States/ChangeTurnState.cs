using Assets.Code.Controllers;
using Assets.Code.Events;
using Assets.Code.Events.Events;
using Assets.Code.UI;
using UnityEngine;

namespace Assets.Code.States.States
{
    public class ChangeTurnState : State
    {
        private TurnHandler turnHandler;
        private CameraHandler cameraHandler;
        private InputHandler inputHandler;

        public ChangeTurnState(StateID currentStateID) : base(currentStateID) { }

        public override void SetTransitions()
        {
            StateTransition selectingUnit = new StateTransition(TransitionID.Next, StateID.SelectUnit);
            AddTransition(selectingUnit);
        }

        public override void SetTransitionEvents()
        {
            StateTransition selectingUnit = GetTransitionByID(TransitionID.Next);
            if (selectingUnit != null)
            {
                // Get Controllers
                turnHandler = TurnHandler.Instance;
                cameraHandler = CameraHandler.Instance;
                inputHandler = InputHandler.Instance;

                // 1. Change active factions
                // 2. Change active units
                // 3. Increment turn counter
                ChangeActiveFactionEvent changeActiveFactionEvent = new ChangeActiveFactionEvent
                    (EventID.ChangeActiveFaction, this, new EventArgs<TurnHandler>(turnHandler));
                selectingUnit.AddEvent(changeActiveFactionEvent, CoroutineID.Execute);
            
                // 4. Display turn GUI
                TurnIndicator turnIndicator = GameObject.Find("Turn Indicator").GetComponent<TurnIndicator>();
                ShowTurnIndicatorEvent showTurnIndicatorEvent = new ShowTurnIndicatorEvent
                    (EventID.ShowTurnIndicator, this, new EventArgs<TurnIndicator, float>(turnIndicator, 3.0f));
                selectingUnit.AddEvent(showTurnIndicatorEvent, CoroutineID.Execute);

                // 5. Pan to nearest active unit
                GameObject nearestActiveUnitObject = null;
                PanCameraToGameObjectEvent panCameraToGameObjectEvent = new PanCameraToGameObjectEvent
                    (EventID.PanCameraToNearestActiveUnit, this, 
                    new EventArgs<CameraHandler, GameObject, float>(cameraHandler, nearestActiveUnitObject, 1.0f));
                selectingUnit.AddEvent(panCameraToGameObjectEvent, CoroutineID.Execute);

                // 6. Enable Input
                EnableInputEvent enableInputEvent = new EnableInputEvent(EventID.EnableInput, this, new EventArgs<InputHandler>(inputHandler));
                selectingUnit.AddEvent(enableInputEvent, CoroutineID.Execute);

                // 7. Enable Camera Drag
            }
        }

        public override void OnEntry()
        {
            base.OnEntry();
            RunEventsByTransitionID(TransitionID.Next);
        }

        public override void Update(float deltaTime) { }
    }
}