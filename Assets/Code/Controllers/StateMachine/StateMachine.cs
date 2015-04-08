using Assets.Code.Controllers.InGame;
using Assets.Code.Controllers.StateMachine.States;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Controllers.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        private bool updateEnabled;
        private bool triggerFired;

        private List<State> states;
        private State currentState;
        private State nextState;
        private State previousState;
        private StateID currentStateID;
        private StateID nextStateID;
        private StateID previousStateID;

        void OnEnable()
        {
            State.OnAnyStateEntry += ResetTrigger;
            State.OnAnyStateEntry += EnableUpdate;
            State.OnAnyStateExit += DisableUpdate;
            State.OnAnyStateExit += ProceedToNextState;
        }

        void OnDestroy()
        {
            State.OnAnyStateEntry -= ResetTrigger;
            State.OnAnyStateEntry -= EnableUpdate;
            State.OnAnyStateExit -= DisableUpdate;
            State.OnAnyStateExit -= ProceedToNextState;
        }

        void Start()
        {
            updateEnabled = false;
            triggerFired = false;

            InitializeStates();
        }

        void Update()
        {
            if (updateEnabled)
                currentState.Update(Time.deltaTime);
        }

        private void InitializeStates()
        {
            states = new List<State>();

            StartBattleState startBattleState = new StartBattleState(StateID.StartingBattle);
            startBattleState.AddTrigger(StateTrigger.BattleStarted, StateID.ChangingTurns);

            ChangeTurnsState changeTurnsState = new ChangeTurnsState(StateID.ChangingTurns);
            changeTurnsState.AddTrigger(StateTrigger.TurnChanged, StateID.SelectingUnit);

            SelectUnitState selectUnitState = new SelectUnitState(StateID.SelectingUnit);
            selectUnitState.AddTrigger(StateTrigger.UnitSelected, StateID.MovingUnit);

            MoveUnitState moveUnitState = new MoveUnitState(StateID.MovingUnit);
            moveUnitState.AddTrigger(StateTrigger.UnitMoved, StateID.SelectingUnitCommand);
            moveUnitState.AddTrigger(StateTrigger.UnitDeselected, StateID.SelectingUnit);

            SelectUnitCommandState selectUnitCommandState = new SelectUnitCommandState(StateID.SelectingUnitCommand);
            selectUnitCommandState.AddTrigger(StateTrigger.UnitCommandSelected, StateID.ConfirmingUnitCommand);
            selectUnitCommandState.AddTrigger(StateTrigger.UnitMoveCancelled, StateID.MovingUnit);

            AddState(startBattleState);
            AddState(changeTurnsState);
            AddState(selectUnitState);
            AddState(moveUnitState);
            AddState(selectUnitCommandState);
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

        private void RemoveState(StateID id)
        {
            if (id != StateID.Null)
            {
                foreach (State state in states)
                {
                    states.Remove(state);
                    return;
                }
            }
        }

        public void Fire(StateTrigger trigger)
        {
            StateID id = currentState.GetOutputState(trigger);
            if (!triggerFired && trigger != StateTrigger.Null && id != StateID.Null)
            {
                Debug.Log("Trigger : " + trigger + " fired");
                triggerFired = true;
                nextStateID = id;
                foreach (State state in states)
                {
                    if (state.StateID == nextStateID)
                    {
                        nextState = state;
                        currentState.OnExit();
                        break;
                    }
                }
            }
        }

        private void ProceedToNextState()
        {
            Debug.Log("Proceeding to next state : " + nextStateID);

            previousStateID = currentStateID;
            previousState = currentState;

            currentStateID = nextStateID;
            currentState = nextState;

            currentState.OnEntry();
        }

        private void EnableUpdate()
        {
            updateEnabled = true;
        }

        private void DisableUpdate()
        {
            updateEnabled = false;
        }

        private void ResetTrigger()
        {
            triggerFired = false;
        }
    }
}