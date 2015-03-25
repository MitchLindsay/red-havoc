using Assets.Code.Generic;
using Assets.Code.Units.Stats;
using UnityEngine;

namespace Assets.Code.TileMaps.Entities
{
    public class Tile : Entity
    {
        public int HealthRegenBonus = 0;
        public int DefenseBonus = 0;
        public int MovementBonus = 0;

        public StatModifier HealthRegenModifier { get; private set; }
        public StatModifier DefenseModifier { get; private set; }
        public StatModifier MovementModifier { get; private set; }

        void Start()
        {
            InitializeStatModifiers();
        }

        private void InitializeStatModifiers()
        {
            HealthRegenModifier = new StatModifier(StatModifierType.Additive, HealthRegenBonus);
            DefenseModifier = new StatModifier(StatModifierType.Additive, DefenseBonus);
            MovementModifier = new StatModifier(StatModifierType.Additive, MovementBonus);
        }
    }
}