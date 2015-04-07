using Assets.Code.Controllers.Abstract;
using Assets.Code.Entities.Pathfinding;
using Assets.Code.Entities.Units;
using Assets.Code.GUI.World;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Controllers.States
{
    public class MoveUnitState : State
    {
        public delegate void StateEntryHandler();
        public static event StateEntryHandler OnStateEntry;
        public static event StateEntryHandler OnStateExit;

        public delegate void DeselectUnitHandler();
        public static event DeselectUnitHandler OnUnitDeselect;

        public delegate void MoveUnitHandler(List<Vector2> path);
        public static event MoveUnitHandler OnUnitMove;

        private Unit selectedUnit = null;
        private List<Vector2> path = null;

        public override void OnInitialized()
        {
            this.StateID = StateID.MovingUnit;
        }

        public override void OnEntry()
        {
            if (OnStateEntry != null)
                OnStateEntry();

            SelectUnitState.OnUnitSelect += SetSelectedUnit;
            Pathfinder.OnPathGenerateComplete += SetPath;
            MouseCursor.OnMouseClickNode += MoveUnitToNode;
        }

        public override void Reason()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                DeselectUnit();
        }

        public override void Update(float deltaTime)
        {

        }

        public override void OnExit()
        {
            SelectUnitState.OnUnitSelect -= SetSelectedUnit;
            Pathfinder.OnPathGenerateComplete -= SetPath;
            MouseCursor.OnMouseClickNode -= MoveUnitToNode;

            if (OnStateExit != null)
                OnStateExit();
        }

        private void DeselectUnit()
        {
            if (OnUnitDeselect != null)
                OnUnitDeselect();

            stateMachine.FireTrigger(StateTrigger.UnitDeselected);
        }

        private void SetSelectedUnit(GameObject gameObject)
        {
            if (gameObject != null)
            {
                Unit unit = gameObject.GetComponent<Unit>();
                if (unit != null)
                    selectedUnit = unit;
            }
        }

        private void SetPath(List<Vector2> path)
        {
            this.path = path;
        }

        private void MoveUnitToNode(GameObject gameObject)
        {
            if (gameObject != null)
            {
                Node node = gameObject.GetComponent<Node>();

                if (node != null && selectedUnit != null && path != null && path.Count > 0 && OnUnitMove != null)
                {
                    stateMachine.FireTrigger(StateTrigger.UnitMoved);
                    OnUnitMove(path);
                }
            }
        }
    }
}