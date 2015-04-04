using Assets.Code.Controllers.Abstract;
using Assets.Code.Entities.Units;
using Assets.Code.GUI.Screen.Command;
using Assets.Code.GUI.World;
using UnityEngine;

namespace Assets.Code.Controllers.States
{
    public class SelectUnitCommandState : State
    {
        public delegate void StateEntryHandler();
        public static event StateEntryHandler OnStateEntry;
        public static event StateEntryHandler OnStateExit;

        public delegate void DeselectUnitHandler();
        public static event DeselectUnitHandler OnUnitDeselect;

        public override void OnInitialized()
        {
            this.StateID = StateID.SelectingUnitCommand;
        }

        public override void OnEntry()
        {
            MouseCursor.OnMouseClickTile += DeselectUnit;
            CommandListWindow.OnCommandButtonClick += InitiateCommand;

            if (OnStateEntry != null)
                OnStateEntry();
        }

        public override void Reason()
        {

        }

        public override void Update(float deltaTime)
        {

        }

        public override void OnExit()
        {
            MouseCursor.OnMouseClickTile -= DeselectUnit;
            CommandListWindow.OnCommandButtonClick -= InitiateCommand;

            if (OnStateExit != null)
                OnStateExit();
        }

        private void DeselectUnit(GameObject gameObject)
        {
            if (gameObject != null)
            {
                if (OnUnitDeselect != null)
                    OnUnitDeselect();

                stateMachine.FireTrigger(StateTrigger.UnitDeselected);
            }
        }

        private void InitiateCommand(Unit unit, UnitCommandType commandType)
        {
            stateMachine.FireTrigger(StateTrigger.UnitCommandSelected);
        }
    }
}
