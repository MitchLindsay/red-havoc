using System.Collections.Generic;

namespace Assets.Code.Units.Abstract
{
    public class Attribute
    {
        public int BaseValue { get; private set; }
        public int FinalValue { get; private set; }
        public List<AttributeModifier> Modifiers { get; private set; }

        public Attribute(int baseValue)
        {
            this.BaseValue = baseValue;
            this.FinalValue = baseValue;
            this.Modifiers = new List<AttributeModifier>();
        }

        public void AddModifier(AttributeModifier modifier)
        {
            if (!Modifiers.Contains(modifier))
            {
                Modifiers.Add(modifier);
                CalculateFinalValue();
            }
        }

        public void RemoveModifier(AttributeModifier modifier)
        {
            if (Modifiers.Contains(modifier))
            {
                Modifiers.Remove(modifier);
                CalculateFinalValue();
            }
        }

        private void CalculateFinalValue()
        {
            FinalValue = BaseValue;

            foreach (AttributeModifier modifier in Modifiers)
            {
                switch(modifier.ModifierType)
                {
                    case ModifierType.Additive:
                        FinalValue += (int)(BaseValue + modifier.ModifierValue);
                        break;
                    case ModifierType.Multiplicative:
                        FinalValue += (int)(BaseValue * modifier.ModifierValue);
                        break;
                    default:
                    case ModifierType.None:
                        FinalValue += 0;
                        break;
                }
            }
        }
    }
}