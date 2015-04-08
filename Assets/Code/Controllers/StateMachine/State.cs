using Assets.Code.Libraries;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Controllers.StateMachine
{
    public enum StateTrigger
    {
        Null,
        BattleStarted,
        TurnChanged,
        UnitSelected,
        UnitDeselected,
        UnitMoved,
        UnitMoveCancelled,
        UnitCommandSelected,
        UnitCommandCancelled,
        UnitCommandConfirmed,
        UnitCommandExecuted,
        BattleEnded
    }

    public enum StateID
    {
        Null,
        StartingBattle,
        ChangingTurns,
        SelectingUnit,
        MovingUnit,
        SelectingUnitCommand,
        ConfirmingUnitCommand,
        ExecutingUnitCommand,
        EndingBattle
    }

    public abstract class State
    {
        public delegate void AnyStateChangeHandler();
        public static event AnyStateChangeHandler OnAnyStateEntry;
        public static event AnyStateChangeHandler OnAnyStateExit;

        public StateID StateID { get; protected set; }
        protected StateMachine stateMachine;
        protected Dictionary<StateTrigger, StateID> map = new Dictionary<StateTrigger, StateID>();

        protected State(StateID stateID)
        {
            this.StateID = stateID;
        }

        public void AddTrigger(StateTrigger trigger, StateID id)
        {
            if (trigger != StateTrigger.Null && id != StateID.Null && !map.ContainsKey(trigger))
                map.Add(trigger, id);
        }

        public void RemoveTrigger(StateTrigger trigger)
        {
            if (trigger != StateTrigger.Null && map.ContainsKey(trigger))
                map.Remove(trigger);
        }

        public StateID GetOutputState(StateTrigger trigger)
        {
            if (map.ContainsKey(trigger))
                return map[trigger];
            else
                return StateID.Null;
        }

        internal void SetStateMachine(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public virtual void OnInitialized() { }

        public virtual void OnEntry()
        {
            Debug.Log("Entering " + StateID);

            if (OnAnyStateEntry != null)
                OnAnyStateEntry();
        }

        public abstract void Update(float deltaTime);

        public virtual void OnExit()
        {
            Debug.Log("Exiting " + StateID);

            if (OnAnyStateExit != null)
                OnAnyStateExit();
        }
    }
}