using Assets.Code.Controllers.Abstract;

namespace Assets.Code.Controllers.States
{
    public class EndBattleState : State
    {
        public override void OnInitialized()
        {
            this.StateID = StateID.EndingBattle;
        }

        public override void OnEntry()
        {
            base.OnEntry();
        }

        public override void Reason()
        {
            stateMachine.FireTrigger(StateTrigger.BattleEnded);
        }

        public override void Update(float deltaTime)
        {

        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
