using Assets.Code.Events;
using UnityEngine;

namespace Assets.Code.States.States
{
    public class ExecuteUnitActionState : State
    {
        public ExecuteUnitActionState(StateID currentStateID) : base(currentStateID) { }

        public override void SetTransitions()
        {
        }

        public override void SetTransitionEvents()
        {
            EventID unitActionID = stateMachine.UnitActionHandler.LastSelectedUnitAction;
            if (unitActionID == EventID.UnitWait)
            {

            }
        }

        public override void OnEntry()
        {
            base.OnEntry();
        }

        public override void Update(float deltaTime)
        {
        }
    }
}