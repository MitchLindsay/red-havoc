
namespace Assets.Code.Units.Abstract
{       
    public enum AttributeModifierType
    {
        None,
        Additive,
        Multiplicative
    }

    public class AttributeModifier
    {
        // Type of modifier
        public AttributeModifierType ModifierType { get; private set; }
        // Value of the modifier to be applied
        public float ModifierValue { get; private set; }

        public AttributeModifier(AttributeModifierType modifierType, float modifierValue)
        {
            this.ModifierType = modifierType;
            this.ModifierValue = modifierValue;
        }
    }
}