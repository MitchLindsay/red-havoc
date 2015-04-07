using Assets.Code.Controllers.Abstract;
using UnityEngine;

namespace Assets.Code.Controllers.States
{
    public class MoveUnitState : State
    {
        public delegate void StateEntryHandler();
        public static event StateEntryHandler OnStateEntry;
        public static event StateEntryHandler OnStateExit;

        public delegate void DeselectUnitHandler();
        public static event DeselectUnitHandler OnUnitDeselect;

        public override void OnInitialized()
        {
            this.StateID = StateID.MovingUnit;
        }

        public override void OnEntry()
        {
            if (OnStateEntry != null)
                OnStateEntry();
        }

        public override void Reason()
        {
            if (Input.GetKey(KeyCode.Escape))
                DeselectUnit();
        }

        public override void Update(float deltaTime)
        {

        }

        public override void OnExit()
        {
            if (OnStateExit != null)
                OnStateExit();
        }

        private void DeselectUnit()
        {
            if (OnUnitDeselect != null)
                OnUnitDeselect();

            stateMachine.FireTrigger(StateTrigger.UnitDeselected);
        }
    }
}