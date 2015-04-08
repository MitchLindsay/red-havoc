using UnityEngine;

namespace Assets.Code.Controllers.StateMachine.States
{
    public class StartBattleState : State
    {
        public delegate void StateChangeHandler();
        public static event StateChangeHandler OnStateEntry;
        public static event StateChangeHandler OnStateExit;

        public StartBattleState(StateID id) : base(id) { }

        public override void OnEntry()
        {
            base.OnEntry();

            if (OnStateEntry != null)
                OnStateEntry();
        }

        public override void Update(float deltaTime)
        {
            stateMachine.Fire(StateTrigger.BattleStarted);
        }

        public override void OnExit()
        {
            if (OnStateExit != null)
                OnStateExit();

            base.OnExit();
        }
    }
}