using Assets.Code.Controllers.Abstract;
using Assets.Code.Entities.Units;
using UnityEngine;

namespace Assets.Code.Controllers.States
{
    public class ChangeTurnState : State
    {
        public delegate void StateEntryHandler();
        public static event StateEntryHandler OnStateEntry;
        public static event StateEntryHandler OnStateExit;

        public override void OnInitialized()
        {
            this.StateID = StateID.ChangingTurns;
        }

        public override void OnEntry()
        {
            TurnHandler.OnFactionChangeComplete += EndTurn;

            if (OnStateEntry != null)
                OnStateEntry();
        }

        public override void Reason()
        {

        }

        public override void Update(float deltaTime)
        {

        }

        public override void OnExit()
        {
            TurnHandler.OnFactionChangeComplete -= EndTurn;
            
            if (OnStateExit != null)
                OnStateExit();
        }

        private void EndTurn(int turnCount, Faction activeFaction)
        {
            if (turnCount > 1)
                Debug.Log("Ending Turn");

            StartTurn(turnCount, activeFaction);
        }

        private void StartTurn(int turnCount, Faction activeFaction)
        {
            Debug.Log("Turn " + turnCount + " : " + activeFaction.FactionName);
            activeFaction.ActivateAllUnits();
            stateMachine.FireTrigger(StateTrigger.TurnChanged);
        }
    }
}
