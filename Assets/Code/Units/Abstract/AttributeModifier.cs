
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
        public ModifierType ModifierType { get; private set; }
        public float ModifierValue { get; private set; }

        public AttributeModifier(ModifierType modifierType, float modifierValue)
        {
            this.ModifierType = modifierType;
            this.ModifierValue = modifierValue;
        }
    }
}