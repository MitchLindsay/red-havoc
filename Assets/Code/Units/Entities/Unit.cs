using Assets.Code.GUI.WorldSpace;
using Assets.Code.TileMaps.Entities;
using UnityEngine;

namespace Assets.Code.Units.Entities
{
    // Unit.cs - A single military unit
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Unit : MonoBehaviour
    {
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
        // Health regenerated per turn, edited through Unity interface
        public int HealthRegen = 0;

        // Modified stats
        public int ModifiedMaxHealth { get; private set; }
        public int ModifiedAttack { get; private set; }
        public int ModifiedAttackRange { get; private set; }
        public int ModifiedDefense { get; private set; }
        public int ModifiedMovement { get; private set; }
        public int ModifiedHealthRegen { get; private set; }

        // Raycast to check for collisions
        private RaycastHit2D raycastHit;

        // Layer masks for units and tiles, edited through Unity interface
        public LayerMask UnitCollisionLayer = -1;
        public LayerMask TileCollisionLayer = -1;

        void Start()
        {
            Health = MaxHealth;
            ModifiedMaxHealth = MaxHealth;
            ModifiedAttack = Attack;
            ModifiedAttackRange = AttackRange;
            ModifiedDefense = Defense;
            ModifiedMovement = Movement;
            ModifiedHealthRegen = HealthRegen;
        }

        void Update()
        {
            CheckRaycastForHits();
        }

        private void CheckRaycastForHits()
        {
            // Check for tile collisions
            raycastHit = Physics2D.Raycast(transform.position + new Vector3(0.5f, 0.5f, 0.0f), Vector2.up, 0.0f, TileCollisionLayer);

            Tile tile = null;
            // If tile collision detected
            if (raycastHit.collider != null)
            {
                tile = raycastHit.collider.gameObject.GetComponent<Tile>();
                if (tile != null)
                    ApplyTileStatBonuses(tile.DefenseBonus, tile.MovementBonus, tile.HealthRegenBonus);
            }
        }

        private void ApplyTileStatBonuses(int defenseBonus, int movementBonus, int healthRegenBonus)
        {
            ModifiedDefense = Defense + defenseBonus;
            ModifiedMovement = Movement + movementBonus;
            ModifiedHealthRegen = HealthRegen + healthRegenBonus;
        }
    }
}