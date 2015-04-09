using Assets.Code.Controllers.InGame;
using Assets.Code.Entities.Units;
using UnityEngine;

namespace Assets.Code.Controllers.StateMachine.States
{
    public class SelectUnitCommandState : State
    {
        public delegate void StateChangeHandler();
        public static event StateChangeHandler OnStateEntry;
        public static event StateChangeHandler OnStateExit;

        public delegate void CancelMoveHandler();
        public static event CancelMoveHandler OnMoveCancel;

        private bool moveCancelled = false;

        public SelectUnitCommandState(StateID id) : base(id) { }

        public override void OnEntry()
        {
            base.OnEntry();

            if (OnStateEntry != null)
                OnStateEntry();

            moveCancelled = false;
            InputHandler.OnBackButtonPress += CancelMove;
            Unit.OnMoveCancel += CancelMoveComplete;
        }

        public override void Update(float deltaTime)
        {
            if (moveCancelled)
                stateMachine.Fire(StateTrigger.UnitMoveCancelled);
        }

        public override void OnExit()
        {
            InputHandler.OnBackButtonPress -= CancelMove;
            Unit.OnMoveCancel -= CancelMoveComplete;

            if (OnStateExit != null)
                OnStateExit();

            base.OnExit();
        }

        private void CancelMove()
        {
            if (OnMoveCancel != null)
                OnMoveCancel();
        }

        private void CancelMoveComplete(GameObject unitObj)
        {
            moveCancelled = true;
        }
    }
}