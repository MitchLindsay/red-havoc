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

        // Base health value of the unit type, edited through Unity interface
        public int Health = 10;

        // Base attack value of the unit type, edited through Unity interface
        public int Attack = 10;

        // Base movement value of the unit type, edited through Unity interface
        public int Movement = 10;
    }
}