using System.Collections.Generic;

namespace Assets.Code.Units.Abstract
{
    public class Attribute
    {
        // Name of the attribute
        public string AttributeName { get; private set; }
        // Unmodified value of the attribute
        public int BaseValue { get; private set; }
        // Modified value of the attribute
        public int ModifiedValue { get; private set; }
        // List of applied modifiers
        public List<AttributeModifier> Modifiers { get; private set; }

        public Attribute(string attributeName, int baseValue)
        {
            this.AttributeName = attributeName;
            this.BaseValue = baseValue;
            this.ModifiedValue = baseValue;
            this.Modifiers = new List<AttributeModifier>();
        }

        // Adds a modifier to the list of modifiers
        public void AddModifier(AttributeModifier modifier)
        {
            // Only add the modifier if it is not already applied
            if (!Modifiers.Contains(modifier))
            {
                Modifiers.Add(modifier);
                CalculateModifiedValue();
            }
        }

        // Removes a modifier from the list of modifiers
        public void RemoveModifier(AttributeModifier modifier)
        {
            // Only remove the modifier if it is already applied
            if (Modifiers.Contains(modifier))
            {
                Modifiers.Remove(modifier);
                CalculateModifiedValue();
            }
        }

        // Removes all modifiers
        public void RemoveAllModifiers()
        {
            Modifiers = new List<AttributeModifier>();
            CalculateModifiedValue();
        }

        // Calculates the modified value using the list of modifiers
        private void CalculateModifiedValue()
        {
            // Create a temporary value to store the calculations
            float tempValue = BaseValue;

            // Loop through each modifier in the list and apply them
            foreach (AttributeModifier modifier in Modifiers)
            {
                switch (modifier.ModifierType)
                {
                    // Add the modifier to the base value
                    case AttributeModifierType.Additive:
                        tempValue += modifier.ModifierValue;
                        break;
                    // Multiply the modifier by the base value
                    case AttributeModifierType.Multiplicative:
                        tempValue *= modifier.ModifierValue;
                        break;
                    // Do nothing
                    case AttributeModifierType.None:
                    default:
                        break;
                }
            }

            // Convert the temporary value to a whole number, then set it to the modified value
            ModifiedValue = (int)tempValue;
        }
    }
}