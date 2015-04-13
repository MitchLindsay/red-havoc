using Assets.Code.Entities.Stats;

namespace Assets.Code.Actors
{
    public class Tile : Actor
    {
        public int HealthRegenBonus = 0;
        public int DefenseBonus = 0;
        public int MovementCost = 0;

        public StatModifier HealthRegenModifier { get; private set; }
        public StatModifier DefenseModifier { get; private set; }

        void Start()
        {
            SetStatModifiers();
        }

        private void SetStatModifiers()
        {
            HealthRegenModifier = new StatModifier(StatModifierType.Additive, HealthRegenBonus);
            DefenseModifier = new StatModifier(StatModifierType.Additive, DefenseBonus);
        }
    }
}