using Assets.Code.Controllers.Abstract;
using Assets.Code.Entities.Units;
using Assets.Code.GUI.Screen.Command;
using Assets.Code.GUI.World;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Controllers.States
{
    public class SelectUnitCommandState : State
    {
        public delegate void StateEntryHandler();
        public static event StateEntryHandler OnStateEntry;
        public static event StateEntryHandler OnStateExit;

        public delegate void CancelMoveHandler(Vector2 start);
        public static event CancelMoveHandler OnMoveCancel;

        private List<Vector2> returnPath = null;

        public override void OnInitialized()
        {
            this.StateID = StateID.SelectingUnitCommand;
        }

        public override void OnEntry()
        {
            MoveUnitState.OnUnitMove += SetReturnPath;

            if (OnStateEntry != null)
                OnStateEntry();
        }

        public override void Reason()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                CancelMove();
        }

        public override void Update(float deltaTime)
        {

        }

        public override void OnExit()
        {
            MoveUnitState.OnUnitMove -= SetReturnPath;

            if (OnStateExit != null)
                OnStateExit();
        }

        private void SetReturnPath(List<Vector2> returnPath)
        {
            this.returnPath = returnPath;
        }

        private void CancelMove()
        {
            if (returnPath != null && returnPath.Count > 0 && OnMoveCancel != null)
            {
                OnMoveCancel(returnPath[0]);
                stateMachine.FireTrigger(StateTrigger.UnitMoveCancelled);
            }
        }
    }
}
