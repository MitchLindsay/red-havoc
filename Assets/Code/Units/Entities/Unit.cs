using UnityEngine;

namespace Assets.Code.Units.Entities
{
    // Unit.cs - A single military unit
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Unit : MonoBehaviour
    {
        // Unique identifier of the unit
        [HideInInspector]
        public int UnitID = -1;

        // Name of the unit, edited through unity interface
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
        // Defense value of the unit type, edited through Unity interface
        public int Defense = 5;
        // Movement value of the unit type, edited through Unity interface
        public int Movement = 10;

        void Start()
        {
            Health = MaxHealth;
        }
    }
}