using Assets.Code.Events;
using Assets.Code.Events.Events;

namespace Assets.Code.States.States
{
    public class StartBattleState : State
    {
        public StartBattleState(StateID currentStateID) : base(currentStateID) { }

        public override void SetTransitions()
        {
            StateTransition changingTurns = new StateTransition(TransitionID.Next, StateID.ChangeTurns);
            AddTransition(changingTurns);
        }

        public override void SetTransitionEvents()
        {
            StateTransition changingTurns = GetTransitionByID(TransitionID.Next);
            if (changingTurns != null)
            {
                WaitEvent waitEvent = new WaitEvent(EventID.Wait, this, new EventArgs<float>(3.0f));
                changingTurns.AddEvent(waitEvent, CoroutineID.Execute);
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