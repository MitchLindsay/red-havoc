using Assets.Code.Controllers;
using Assets.Code.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.States
{
    public enum StateID
    {
        Null,
        StartBattle,
        ChangeTurns,
        SelectUnit,
        MoveUnit,
        SelectBackMenuOption,
        SelectUnitAction,
        ConfirmUnitAction,
        ExecuteUnitAction,
        EndBattle
    }

    public abstract class State
    {
        public StateID StateID { get; private set; }
        public List<StateTransition> Transitions { get; private set; }
        protected StateMachine stateMachine;

        public State(StateID stateID)
        {
            this.StateID = stateID;
            RemoveAllTransitions();
        }

        public abstract void SetTransitions();
        public abstract void SetTransitionEvents();
        public abstract void Update(float deltaTime);

        public virtual void OnEntry()
        {
            Debug.Log("Entered " + StateID);
            RemoveAllTransitions();
            SetTransitions();
            SetTransitionEvents();
        }

        public virtual void OnExit()
        {
            Debug.Log("Exited " + StateID);
            RemoveAllTransitions();
        }

        internal void SetStateMachine(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        protected void AddTransition(StateTransition transition)
        {
            if (transition != null && !Transitions.Contains(transition))
                Transitions.Add(transition);
        }

        protected void RemoveTransition(TransitionID transitionID)
        {
            if (transitionID != TransitionID.Null)
            {
                StateTransition transition = GetTransitionByID(transitionID);

                if (transition != null)
                    Transitions.Remove(transition);
            }
        }

        protected void RemoveAllTransitions()
        {
            Transitions = new List<StateTransition>();
        }

        protected StateTransition GetTransitionByID(TransitionID transitionID)
        {
            if (transitionID != TransitionID.Null && Transitions.Count > 0)
            {
                foreach (StateTransition transition in Transitions)
                {
                    if (transition.TransitionID == transitionID)
                        return transition;
                }
            }

            return null;
        }

        public void RunEventsByTransitionID(TransitionID transitionID)
        {
            StateTransition transition = GetTransitionByID(transitionID);
            if (transition != null)
                transition.RunEvents();
        }
    }
}