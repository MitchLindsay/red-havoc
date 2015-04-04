using Assets.Code.Controllers.Abstract;
using Assets.Code.Controllers.States;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Controllers
{
    public class StateMachine : MonoBehaviour
    {
        #pragma warning disable 0414
        private List<State> states;
        private State currentState;
        private StateID currentStateID;
        private State previousState;
        private StateID previousStateID;
        #pragma warning restore 0414

        void Start()
        {
            InitializeStates();
        }

        void Update()
        {
            currentState.Reason();
            currentState.Update(Time.deltaTime);
        }

        private void InitializeStates()
        {
            states = new List<State>();

            StartBattleState startBattleState = new StartBattleState();
            startBattleState.AddTrigger(StateTrigger.BattleStarted, StateID.ChangingTurns);

            ChangeTurnState changeTurnState = new ChangeTurnState();
            changeTurnState.AddTrigger(StateTrigger.TurnChanged, StateID.SelectingUnit);

            SelectUnitState selectUnitState = new SelectUnitState();
            selectUnitState.AddTrigger(StateTrigger.UnitSelected, StateID.SelectingUnitCommand);

            SelectUnitCommandState selectUnitCommandState = new SelectUnitCommandState();
            selectUnitCommandState.AddTrigger(StateTrigger.UnitCommandSelected, StateID.ConfirmingUnitCommand);
            selectUnitCommandState.AddTrigger(StateTrigger.UnitDeselected, StateID.SelectingUnit);

            ConfirmUnitCommandState confirmUnitCommandState = new ConfirmUnitCommandState();
            confirmUnitCommandState.AddTrigger(StateTrigger.UnitCommandConfirmed, StateID.ExecutingUnitCommand);

            ExecuteUnitCommandState executeUnitCommandState = new ExecuteUnitCommandState();
            executeUnitCommandState.AddTrigger(StateTrigger.UnitCommandExecuted, StateID.SelectingUnit);

            EndBattleState endBattleState = new EndBattleState();
            endBattleState.AddTrigger(StateTrigger.BattleEnded, StateID.Null);

            AddState(startBattleState);
            AddState(changeTurnState);
            AddState(selectUnitState);
            AddState(selectUnitCommandState);
            AddState(confirmUnitCommandState);
            AddState(executeUnitCommandState);
            AddState(endBattleState);
        }

        private void AddState(State state)
        {
            if (state != null && !states.Contains(state))
            {
                state.SetStateMachine(this);

                if (states.Count == 0)
                {
                    currentState = state;
                    currentStateID = state.StateID;
                    currentState.OnEntry();
                }

                states.Add(state);
            }
        }

        private void RemoveState(StateID stateID)
        {
            if (stateID != StateID.Null)
            {
                foreach (State state in states)
                {
                    if (state.StateID == stateID)
                    {
                        states.Remove(state);
                        return;
                    }
                }
            }
        }

        public void FireTrigger(StateTrigger trigger)
        {
            StateID stateID = currentState.GetOutputState(trigger);

            if (trigger != StateTrigger.Null && stateID != StateID.Null)
            {
                previousStateID = currentStateID;
                currentStateID = stateID;

                foreach (State state in states)
                {
                    if (state.StateID == currentStateID)
                    {
                        currentState.OnExit();

                        previousState = currentState;
                        currentState = state;
                        currentState.OnEntry();

                        Debug.Log("Entered " + currentState.StateID);
                        break;
                    }
                }
            }
        }
    }
}