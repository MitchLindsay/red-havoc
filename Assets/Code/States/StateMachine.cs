using Assets.Code.Generic;
using Assets.Code.States.States;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.States
{
    public class StateMachine : Singleton<StateMachine>
    {
        public State CurrentState { get; private set; }
        public StateID CurrentStateID { get; private set; }
        private List<State> states;

        void OnEnable()
        {
            StateTransition.OnEventsComplete += TransitionToNextState;
        }

        void OnDestroy()
        {
            StateTransition.OnEventsComplete -= TransitionToNextState;
        }

        void Start()
        {
            MapStates();
        }

        void Update()
        {
            CurrentState.Update(Time.deltaTime);
        }

        private void MapStates()
        {
            states = new List<State>();

            StartBattleState startingBattle = new StartBattleState(StateID.StartBattle);
            ChangeTurnState changingTurns = new ChangeTurnState(StateID.ChangeTurns);
            SelectUnitState selectingUnit = new SelectUnitState(StateID.SelectUnit);
            SelectBackMenuOptionState selectingBackMenuOption = new SelectBackMenuOptionState(StateID.SelectBackMenuOption);
            MoveUnitState movingUnit = new MoveUnitState(StateID.MoveUnit);
            SelectUnitActionState selectingUnitAction = new SelectUnitActionState(StateID.SelectUnitAction);
            ConfirmUnitActionState confirmingUnitAction = new ConfirmUnitActionState(StateID.ConfirmUnitAction);
            ExecuteUnitActionState executingUnitAction = new ExecuteUnitActionState(StateID.ExecuteUnitAction);
            EndBattleState endingBattle = new EndBattleState(StateID.EndBattle);

            AddState(startingBattle);
            AddState(changingTurns);
            AddState(selectingUnit);
            AddState(selectingBackMenuOption);
            AddState(movingUnit);
            AddState(selectingUnitAction);
            AddState(confirmingUnitAction);
            AddState(executingUnitAction);
            AddState(endingBattle);
        }

        private void AddState(State state)
        {
            if (state != null && !states.Contains(state))
            {
                state.SetStateMachine(this);

                if (states.Count == 0)
                    ChangeStateTo(state, state.StateID);

                states.Add(state);
            }
        }

        private void RemoveState(StateID stateID)
        {
            if (stateID != StateID.Null)
            {
                State state = GetStateByID(stateID);

                if (state != null)
                    states.Remove(state);
            }
        }

        private State GetStateByID(StateID stateID)
        {
            if (stateID != StateID.Null && states.Count > 0)
            {
                foreach (State state in states)
                {
                    if (state.StateID == stateID)
                        return state;
                }
            }

            return null;
        }

        public void TransitionToNextState(StateID nextStateID)
        {
            if (nextStateID != StateID.Null)
            {
                State nextState = GetStateByID(nextStateID);
                ChangeStateTo(nextState, nextStateID);
            }
        }

        private void ChangeStateTo(State nextState, StateID nextStateID)
        {
            if (CurrentState != null)
                CurrentState.OnExit();

            Debug.Log("Changing from " + CurrentStateID + " to " + nextStateID);

            CurrentState = nextState;
            CurrentStateID = nextStateID;

            CurrentState.OnEntry();
        }
    }
}