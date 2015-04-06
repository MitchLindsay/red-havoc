using Assets.Code.Controllers.Abstract;
using Assets.Code.GUI.World;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Controllers.States
{
    public class MoveUnitState : State
    {
        public delegate void StateEntryHandler();
        public delegate void NodeChangeHandler();
        public static event StateEntryHandler OnStateEntry;
        public static event StateEntryHandler OnStateExit;
        public static event NodeChangeHandler OnNodeChange;

        private GameObject previousNodeObj = null;
        private GameObject currentNodeObj = null;

        public override void OnInitialized()
        {
            this.StateID = StateID.MovingUnit;
        }

        public override void OnEntry()
        {
            MouseCursor.OnMouseOverNode += SetNodeObject;

            if (OnStateEntry != null)
                OnStateEntry();
        }

        public override void Reason()
        {

        }

        public override void Update(float deltaTime)
        {
            if (currentNodeObj != previousNodeObj && OnNodeChange != null)
                OnNodeChange();
        }

        public override void OnExit()
        {
            MouseCursor.OnMouseOverNode -= SetNodeObject;

            if (OnStateExit != null)
                OnStateExit();
        }

        private void SetNodeObject(GameObject gameObject)
        {
            currentNodeObj = gameObject;
        }
    }
}