
namespace Assets.Code.Units.Stats
{       
    public enum StatModifierType
    {
        None,
        Additive,
        Multiplicative
    }

    public class StatModifier
    {
        public StatModifierType ModifierType { get; private set; }
        public float ModifierValue { get; private set; }

        public StatModifier(StatModifierType modifierType, float modifierValue)
        {
            this.ModifierType = modifierType;
            this.ModifierValue = modifierValue;
        }
    }
}