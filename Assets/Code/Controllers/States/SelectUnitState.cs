using Assets.Code.Controllers.Abstract;
using Assets.Code.Entities.Units;
using Assets.Code.Entities.Units.Commands;
using Assets.Code.GUI.World;
using UnityEngine;

namespace Assets.Code.Controllers.States
{
    public class SelectUnitState : State
    {
        public delegate void StateEntryHandler();
        public static event StateEntryHandler OnStateEntry;
        public static event StateEntryHandler OnStateExit;

        public delegate void SelectUnitHandler(GameObject gameObject);
        public static event SelectUnitHandler OnUnitSelect;

        public delegate void DeselectUnitHandler();
        public static event DeselectUnitHandler OnUnitDeselect;

        public override void OnInitialized()
        {
            this.StateID = StateID.SelectingUnit;
        }

        public override void OnEntry()
        {
            MouseCursor.OnMouseClickUnit += SelectUnit;

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
            MouseCursor.OnMouseClickUnit -= SelectUnit;

            if (OnStateExit != null)
                OnStateExit();
        }

        private void SelectUnit(GameObject gameObject)
        {
            if (gameObject != null)
            {
                Unit unit = gameObject.GetComponent<Unit>();
                if (unit != null && unit.IsActive)
                {
                    if (OnUnitSelect != null)
                        OnUnitSelect(gameObject);

                    stateMachine.FireTrigger(StateTrigger.UnitSelected);
                }
            }
        }

        private void DeselectUnit()
        {
            if (OnUnitDeselect != null)
                OnUnitDeselect();
        }
    }
}