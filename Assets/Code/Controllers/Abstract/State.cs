using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Controllers.Abstract
{
    public enum StateTrigger
    {
        Null = 0,
        BattleStarted = 1,
        TurnChanged = 2,
        UnitSelected = 3,
        UnitCommandSelected = 4,
        UnitCommandConfirmed = 5,
        UnitCommandExecuted = 6,
        BattleEnded = 7,
        UnitDeselected = 8,
    }

    public enum StateID
    {
        Null = 0,
        StartingBattle = 1,
        ChangingTurns = 2,
        SelectingUnit = 3,
        SelectingUnitCommand = 4,
        ConfirmingUnitCommand = 5,
        ExecutingUnitCommand = 6,
        EndingBattle = 7,
    }

    public abstract class State
    {
        public StateID StateID { get; protected set; }
        protected StateMachine stateMachine;
        protected Dictionary<StateTrigger, StateID> map = new Dictionary<StateTrigger, StateID>();

        public void AddTrigger(StateTrigger stateTrigger, StateID stateID)
        {
            if (stateTrigger != StateTrigger.Null && stateID != StateID.Null && !map.ContainsKey(stateTrigger))
                map.Add(stateTrigger, stateID);
        }

        public void RemoveTrigger(StateTrigger stateTrigger)
        {
            if (stateTrigger != StateTrigger.Null && map.ContainsKey(stateTrigger))
                map.Remove(stateTrigger);
        }

        public StateID GetOutputState(StateTrigger stateTrigger)
        {
            if (map.ContainsKey(stateTrigger))
                return map[stateTrigger];
            else
                return StateID.Null;
        }

        internal void SetStateMachine(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            OnInitialized();
        }

        public virtual void OnInitialized() { }
        public virtual void OnEntry() { }
        public virtual void Reason() { }
        public abstract void Update(float deltaTime);
        public virtual void OnExit() { }
    }
}