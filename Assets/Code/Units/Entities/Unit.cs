using Assets.Code.Generic;
using Assets.Code.TileMaps.Entities;
using Assets.Code.Units.Stats;
using System;
using UnityEngine;

namespace Assets.Code.Units.Entities
{
    public class Unit : Entity
    {
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
                ApplyTileStatModifiers(tile);
            }
        }

        private void ApplyTileStatModifiers(Tile tile)
        {
            HealthRegen.AddModifier(tile.HealthRegenModifier);
            Defense.AddModifier(tile.DefenseModifier);
            Movement.AddModifier(tile.MovementModifier);
        }
    }
}