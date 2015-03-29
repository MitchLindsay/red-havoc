using Assets.Code.Controllers.Abstract;

namespace Assets.Code.Controllers.States
{
    public class StartBattleState : State
    {
        public delegate void StateEntryHandler();
        public static event StateEntryHandler OnStateEntry;
        public static event StateEntryHandler OnStateExit;

        public override void OnInitialized()
        {
            this.StateID = StateID.StartingBattle;
        }

        public override void OnEntry()
        {
            if (OnStateEntry != null)
                OnStateEntry();
        }

        public override void Reason()
        {
            stateMachine.FireTrigger(StateTrigger.BattleStarted);
        }

        public override void Update(float deltaTime)
        {

        }

        public override void OnExit()
        {
            if (OnStateExit != null)
                OnStateExit();
        }
    }
}
