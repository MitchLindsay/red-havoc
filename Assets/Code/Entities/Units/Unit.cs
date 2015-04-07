using Assets.Code.Controllers.States;
using Assets.Code.Entities.Abstract;
using Assets.Code.Entities.Stats;
using Assets.Code.Entities.Tiles;
using Assets.Code.Libraries;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Entities.Units
{
    public class Unit : Entity
    {
        public delegate void MoveHandler();
        public delegate void MoveCancelHandler(GameObject gameObject);
        public static event MoveHandler OnMoveStart;
        public static event MoveHandler OnMoveStop;
        public static event MoveCancelHandler OnMoveCancel;

        public Faction Faction { get; private set; }
        public List<UnitCommand> ActiveCommands { get; private set; }

        [HideInInspector]
        public bool IsActive = false;
        [HideInInspector]
        public bool IsSelected = false;
        [HideInInspector]
        public int Health = 10;

        public float MoveSpeed = 2.0f;

        public int BaseHealth = 10;
        public int BaseHealthRegen = 0;
        public int BaseAttack = 5;
        public int BaseAttackRange = 1;
        public int BaseDefense = 3;
        public int BaseMovement = 6;

        public Stat MaxHealth { get; private set; }
        public Stat HealthRegen { get; private set; }
        public Stat Attack { get; private set; }
        public Stat AttackRange { get; private set; }
        public Stat Defense { get; private set; }
        public Stat Movement { get; private set; }

        void OnEnable()
        {
            SelectUnitState.OnUnitSelect += SetToSelected;
            SelectUnitState.OnUnitDeselect += SetToDeselected;
            MoveUnitState.OnUnitMove += Move;
            SelectUnitCommandState.OnMoveCancel += CancelMove;
        }

        void OnDestroy()
        {
            SelectUnitState.OnUnitSelect -= SetToSelected;
            SelectUnitState.OnUnitDeselect -= SetToDeselected;
            MoveUnitState.OnUnitMove -= Move;
            SelectUnitCommandState.OnMoveCancel -= CancelMove;
        }

        void Awake()
        {
            InitializeStats();
            RemoveAllActiveCommands();
        }

        void Update()
        {
            CheckForCollisions<Tile>(TileLayerMask);
        }

        private void InitializeStats()
        {
            MaxHealth = new Stat("Max Health", BaseHealth);
            HealthRegen = new Stat("Health Regen", BaseHealthRegen);
            Attack = new Stat("Attack", BaseAttack);
            AttackRange = new Stat("Attack Range", BaseAttackRange);
            Defense = new Stat("Defense", BaseDefense);
            Movement = new Stat("Movement", BaseMovement);

            Health = MaxHealth.ModifiedValue;
        }

        public override void HandleCollision<TEntity>(GameObject collidedGameObject)
        {
            if (typeof(TEntity) == typeof(Tile) && collidedGameObject != null)
            {
                Tile tile = collidedGameObject.GetComponent<Tile>();

                HealthRegen.AddModifier(tile.HealthRegenModifier);
                Defense.AddModifier(tile.DefenseModifier);
            }
        }

        public void SetFaction(Faction faction)
        {
            this.Faction = faction;
        }

        private void SetToSelected(GameObject selectedObject)
        {
            if (selectedObject != null)
            {
                Unit unit = selectedObject.GetComponent<Unit>();
                if (unit == this && IsActive)
                    IsSelected = true;
            }
        }

        private void SetToDeselected()
        {
            IsSelected = false;
        }

        public void AddActiveCommand(UnitCommand command)
        {
            if (command != null && !ActiveCommands.Contains(command))
                ActiveCommands.Add(command);
        }

        public void RemoveActiveCommand(UnitCommand command)
        {
            if (ActiveCommands.Contains(command))
                ActiveCommands.Remove(command);
        }

        public void RemoveAllActiveCommands()
        {
            ActiveCommands = new List<UnitCommand>();
        }

        public bool IsCommandAvailable(UnitCommandType commandType)
        {
            UnitCommand command = Faction.GetCommand(commandType);
            if (command != null && ActiveCommands.Contains(command))
                return true;

            return false;
        }

        public void Move(List<Vector2> path)
        {
            if (path != null && IsSelected && IsActive)
            {
                if (OnMoveStart != null)
                    OnMoveStart();

                Job moveJob = Job.Make(MoveCoroutine(path, MoveSpeed), true);

                moveJob.JobComplete += (wasKilled) =>
                {
                    if (OnMoveStop != null)
                        OnMoveStop();
                };
            }
        }

        private IEnumerator MoveCoroutine(List<Vector2> path, float speed)
        {
            Vector2 unitPosition = gameObject.transform.position;

            for (int i = 0; i < path.Count; i++)
            {
                float timeElapsed = 0.0f;
                Vector3 destination = path[i] - new Vector2(0.5f, 0.5f);
                Vector3 startPosition = unitPosition;

                while (timeElapsed < 1.0f)
                {
                    timeElapsed += Time.deltaTime * (Time.timeScale / speed);
                    unitPosition = Vector3.Lerp(startPosition, destination, timeElapsed);

                    gameObject.transform.position = unitPosition;
                    yield return null;
                }
            }

            yield return null;
        }

        private void CancelMove(Vector2 start)
        {
            if (start != null && IsSelected && IsActive)
            {
                this.transform.position = start - new Vector2(0.5f, 0.5f);

                if (OnMoveCancel != null)
                    OnMoveCancel(this.gameObject);
            }
        }
    }
}