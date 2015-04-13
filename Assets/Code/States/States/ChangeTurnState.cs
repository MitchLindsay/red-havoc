using Assets.Code.Events;
using Assets.Code.Events.Events;

namespace Assets.Code.States.States
{
    public class ChangeTurnState : State
    {
        public ChangeTurnState(StateID currentStateID) : base(currentStateID) { }

        public override void SetTransitions()
        {
            StateTransition selectingUnit = new StateTransition(TransitionID.Next, StateID.SelectUnit);
            AddTransition(selectingUnit);
        }

        public override void SetTransitionEvents()
        {
            StateTransition selectingUnit = GetTransitionByID(TransitionID.Next);
            if (selectingUnit != null)
            {
                // 1. Change active factions
                // 2. Change active units
                // 3. Increment turn counter
                // 4. Display turn GUI

                // 5. Wait 1 second
                WaitEvent waitEvent = new WaitEvent(EventID.Wait, this, new EventArgs<float>(1.0f));

                // 6. Pan to nearest active unit
                // 7. Proceed to next turn

                selectingUnit.AddEvent(waitEvent, CoroutineID.Execute);
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