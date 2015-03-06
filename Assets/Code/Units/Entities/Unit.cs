using Assets.Code.Units.Abstract;
using UnityEngine;

namespace Assets.Code.Units.Entities
{
    // Unit.cs - A single military unit
    public class Unit : Entity
    {
        // Current health of the unit
        [HideInInspector]
        public int Health = 10;

        // Max health value of the unit type, edited through Unity interface
        public int BaseMaxHealth = 10;
        // Attack value of the unit type, edited through Unity interface
        public int BaseAttack = 10;
        // Attack range value of the unit type, edited through Unity interface
        public int BaseAttackRange = 1;
        // Movement value of the unit type, edited through Unity interface
        public int BaseMovement = 10;

        // Unit attributes
        public Attribute MaxHealth { get; set; }
        public Attribute Attack { get; set; }
        public Attribute AttackRange { get; set; }
        public Attribute Movement { get; set; }

        void Start()
        {
            this.MaxHealth = new Attribute("Max Health", BaseMaxHealth);
            this.Attack = new Attribute("Attack", BaseAttack);
            this.AttackRange = new Attribute("Attack Range", BaseAttackRange);
            this.Movement = new Attribute("Movement", BaseMovement);

            this.Health = MaxHealth.ModifiedValue;
        }
    }
}