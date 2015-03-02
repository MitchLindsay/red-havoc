
namespace Assets.Code.Units.Abstract
{       
    public enum ModifierType
    {
        None,
        Additive,
        Multiplicative
    }

    public class AttributeModifier
    {
        // Type of modifier
        public ModifierType ModifierType { get; private set; }
        // Value of the modifier to be applied
        public float ModifierValue { get; private set; }

        public AttributeModifier(ModifierType modifierType, float modifierValue)
        {
            this.ModifierType = modifierType;
            this.ModifierValue = modifierValue;
        }
    }
}