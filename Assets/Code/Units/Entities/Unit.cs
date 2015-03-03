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

        // Max health value of the unit type, edited through Unity interface
        public int MaxHealth = 10;
        // Attack value of the unit type, edited through Unity interface
        public int Attack = 10;
        // Attack range value of the unit type, edited through Unity interface
        public int AttackRange = 1;
        // Movement value of the unit type, edited through Unity interface
        public int Movement = 10;

        // Unit attributes
        public Attribute MaxHealth_Attribute { get; set; }
        public Attribute Attack_Attribute { get; set; }
        public Attribute AttackRange_Attribute { get; set; }
        public Attribute Movement_Attribute { get; set; }

        void Start()
        {
            this.MaxHealth_Attribute = new Attribute(MaxHealth);
            this.Attack_Attribute = new Attribute(Attack);
            this.AttackRange_Attribute = new Attribute(AttackRange);
            this.Movement_Attribute = new Attribute(Movement);

            this.Health = MaxHealth_Attribute.FinalValue;
        }
    }
}