using Assets.Code.Units.Abstract;
using UnityEngine;

namespace Assets.Code.Units.Entities
{
    [RequireComponent (typeof(SpriteRenderer))]
    public class Unit : MonoBehaviour
    {
        // Unique identifier for the unit
        [HideInInspector]
        public int UnitID = -1;

        // Name of the unit type, edited through Unity interface
        public string UnitName = "Unit";

        // Current health of the unit
        [HideInInspector]
        public int Health = 10;

        // Base max health value of the unit type, edited through Unity interface
        public int BaseMaxHealth = 10;
        // Base attack value of the unit type, edited through Unity interface
        public int BaseAttack = 10;
        // Base attack range value of the unit type, edited through Unity interface
        public int BaseAttackRange = 1;
        // Base movement value of the unit type, edited through Unity interface
        public int BaseMovement = 10;

        // Unit attributes
        public Attribute MaxHealth { get; set; }
        public Attribute Attack { get; set; }
        public Attribute AttackRange { get; set; }
        public Attribute Movement { get; set; }

        void Start()
        {
            this.MaxHealth = new Attribute(BaseMaxHealth);
            this.Attack = new Attribute(BaseAttack);
            this.AttackRange = new Attribute(BaseAttackRange);
            this.Movement = new Attribute(BaseMovement);

            this.Health = MaxHealth.FinalValue;
        }
    }
}