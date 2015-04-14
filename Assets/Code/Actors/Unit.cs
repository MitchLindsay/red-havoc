using Assets.Code.Controllers;
using Assets.Code.Entities.Stats;
using UnityEngine;

namespace Assets.Code.Actors
{
    public class Unit : Actor
    {
        public int BaseHealth = 10;
        public int BaseHealthRegen = 0;
        public int BaseAttack = 5;
        public int BaseAttackRange = 1;
        public int BaseDefense = 3;
        public int BaseMovement = 6;

        public bool IsActive { get; set; }
        public int Health { get; private set; }
        public Stat MaxHealth { get; private set; }
        public Stat HealthRegen { get; private set; }
        public Stat Attack { get; private set; }
        public Stat AttackRange { get; private set; }
        public Stat Defense { get; private set; }
        public Stat Movement { get; private set; }

        void Awake()
        {
            SetStats();
        }

        void Update()
        {
            CheckForCollisions<Tile>(layerMasks.TileMask);
        }

        private void SetStats()
        {
            MaxHealth = new Stat("Max Health", BaseHealth);
            HealthRegen = new Stat("Health Regen", BaseHealthRegen);
            Attack = new Stat("Attack", BaseAttack);
            AttackRange = new Stat("Attack Range", BaseAttackRange);
            Defense = new Stat("Defense", BaseDefense);
            Movement = new Stat("Movement", BaseMovement);

            Health = MaxHealth.ModifiedValue;
        }

        public override void HandleCollision<T>(GameObject collidedObject)
        {
            if (collidedObject != null)
            {
                // Handle Tile Collisions
                if (typeof(T) == typeof(Tile))
                    ApplyTileStatModifiers(collidedObject.GetComponent<Tile>());
            }
        }

        private void ApplyTileStatModifiers(Tile tile)
        {
            if (tile != null)
            {
                HealthRegen.AddModifier(tile.HealthRegenModifier);
                Defense.AddModifier(tile.DefenseModifier);
            }
        }
    }
}