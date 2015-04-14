using Assets.Code.Actors;
using Assets.Code.Events;

namespace Assets.Code.States.States
{
    public class SelectUnitState : State
    {
        public SelectUnitState(StateID currentStateID) : base(currentStateID) { }

        public override void SetTransitions()
        {
            StateTransition movingUnit = new StateTransition(TransitionID.Next, StateID.MoveUnit);
            AddTransition(movingUnit);
            
            // Transition to change turns, manually end turns from "back" menu
        }

        public override void SetTransitionEvents()
        {

        }

        public override void Update(float deltaTime)
        {
        }
    }
}