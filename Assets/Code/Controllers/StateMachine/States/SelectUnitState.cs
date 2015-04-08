using Assets.Code.Controllers.InGame;
using Assets.Code.Entities.Units;
using Assets.Code.GUI.World;
using UnityEngine;

namespace Assets.Code.Controllers.StateMachine.States
{
    public class SelectUnitState : State
    {
        public delegate void StateChangeHandler();
        public static event StateChangeHandler OnStateEntry;
        public static event StateChangeHandler OnStateExit;

        public delegate void UnitSelectionHandler(GameObject gameObject);
        public static event UnitSelectionHandler OnUnitSelect;

        public SelectUnitState(StateID id) : base(id) { }

        public override void OnEntry()
        {
            MouseCursor.OnMouseClickUnit += SelectUnit;
            CameraHandler.OnPanStop += UnitSelectComplete;

            base.OnEntry();

            if (OnStateEntry != null)
                OnStateEntry();
        }

        public override void Update(float deltaTime) { }

        public override void OnExit()
        {
            MouseCursor.OnMouseClickUnit -= SelectUnit;
            CameraHandler.OnPanStop -= UnitSelectComplete;

            if (OnStateExit != null)
                OnStateExit();

            base.OnExit();
        }

        private void SelectUnit(GameObject unitObj)
        {
            if (unitObj != null)
            {
                Unit unit = unitObj.GetComponent<Unit>();
                if (unit != null)
                {
                    if (OnUnitSelect != null)
                        OnUnitSelect(unitObj);
                }
            }
        }

        private void UnitSelectComplete()
        {
            stateMachine.Fire(StateTrigger.UnitSelected);
        }
    }
}