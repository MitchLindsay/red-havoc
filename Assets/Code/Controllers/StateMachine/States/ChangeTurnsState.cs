using Assets.Code.Controllers.InGame;
using Assets.Code.Entities.Units;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Controllers.StateMachine.States
{
    public class ChangeTurnsState : State
    {
        public delegate void StateChangeHandler();
        public static event StateChangeHandler OnStateEntry;
        public static event StateChangeHandler OnStateExit;

        public ChangeTurnsState(StateID id) : base(id) { }

        public override void OnEntry()
        {
            TurnHandler.OnFactionChangeComplete += EndPreviousTurn;

            base.OnEntry();

            if (OnStateEntry != null)
                OnStateEntry();
        }

        public override void Update(float deltaTime) { }

        public override void OnExit()
        {
            TurnHandler.OnFactionChangeComplete -= EndPreviousTurn;

            if (OnStateExit != null)
                OnStateExit();

            base.OnExit();
        }

        private void EndPreviousTurn(int turnCount, Faction activeFaction)
        {
            if (turnCount > 1)
                Debug.Log("Ending Turn");

            StartNextTurn(turnCount, activeFaction);
        }

        private void StartNextTurn(int turnCount, Faction activeFaction)
        {
            Debug.Log("Turn " + turnCount + " : " + activeFaction.FactionName);

            activeFaction.ActivateAllUnits();
            stateMachine.Fire(StateTrigger.TurnChanged);
        }
    }
}