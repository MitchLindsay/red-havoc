using Assets.Code.Controllers.InGame;
using Assets.Code.Entities.Pathfinding;
using Assets.Code.Entities.Units;
using Assets.Code.GUI.World;
using UnityEngine;

namespace Assets.Code.Controllers.StateMachine.States
{
    public class MoveUnitState : State
    {
        public delegate void StateChangeHandler();
        public static event StateChangeHandler OnStateEntry;
        public static event StateChangeHandler OnStateExit;

        public delegate void UnitMoveHandler();
        public static event UnitMoveHandler OnUnitMove;

        public delegate void UnitDeselectionHandler();
        public static event UnitDeselectionHandler OnUnitDeselect;

        public MoveUnitState(StateID id) : base(id) { }

        public override void OnEntry()
        {
            base.OnEntry();

            if (OnStateEntry != null)
                OnStateEntry();

            InputHandler.OnBackButtonPress += DeselectUnit;
            MouseCursor.OnMouseClickNode += MoveUnitToNode;
            Unit.OnMoveStop += MoveUnitComplete;
        }

        public override void Update(float deltaTime) { }

        public override void OnExit()
        {
            InputHandler.OnBackButtonPress -= DeselectUnit;
            MouseCursor.OnMouseClickNode -= MoveUnitToNode;
            Unit.OnMoveStop -= MoveUnitComplete;

            if (OnStateExit != null)
                OnStateExit();

            base.OnExit();
        }

        private void MoveUnitToNode(GameObject nodeObj)
        {
            if (nodeObj != null)
            {
                Node node = nodeObj.GetComponent<Node>();
                if (node != null && OnUnitMove != null)
                    OnUnitMove();
            }
        }

        private void MoveUnitComplete()
        {
            stateMachine.Fire(StateTrigger.UnitMoved);
        }

        private void DeselectUnit()
        {
            if (OnUnitDeselect != null)
                OnUnitDeselect();

            stateMachine.Fire(StateTrigger.UnitDeselected);
        }
    }
}