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
        public Faction Faction { get; private set; }

        [HideInInspector]
        public bool IsActive = false;
        [HideInInspector]
        public int Health = 10;

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

        void Start()
        {
            InitializeStats();
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
                Movement.AddModifier(tile.MovementModifier);
            }
        }

        public void SetFaction(Faction faction)
        {
            this.Faction = faction;
        }
    }
}