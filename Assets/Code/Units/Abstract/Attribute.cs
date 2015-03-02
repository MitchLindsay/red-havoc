using System.Collections.Generic;

namespace Assets.Code.Units.Abstract
{
    public class Attribute
    {
        // Unmodified value of the attribute
        public int BaseValue { get; private set; }
        // Modified value of the attribute
        public int FinalValue { get; private set; }
        // List of applied modifiers
        public List<AttributeModifier> Modifiers { get; private set; }

        public Attribute(int baseValue)
        {
            this.BaseValue = baseValue;
            this.FinalValue = baseValue;
            this.Modifiers = new List<AttributeModifier>();
        }

        // Adds a modifier to the list of modifiers
        public void AddModifier(AttributeModifier modifier)
        {
            // Only add the modifier if it is not already applied
            if (!Modifiers.Contains(modifier))
            {
                Modifiers.Add(modifier);
                CalculateFinalValue();
            }
        }

        // Removes a modifier from the list of modifiers
        public void RemoveModifier(AttributeModifier modifier)
        {
            // Only remove the modifier if it is already applied
            if (Modifiers.Contains(modifier))
            {
                Modifiers.Remove(modifier);
                CalculateFinalValue();
            }
        }

        // Removes all modifiers
        public void RemoveAllModifiers()
        {
            Modifiers = new List<AttributeModifier>();
            CalculateFinalValue();
        }

        // Calculates the final value using the list of modifiers
        private void CalculateFinalValue()
        {
            // Create a temporary value to store the calculations
            float tempValue = 0.0f;

            // If modifiers exist, apply them
            if (Modifiers.Count > 0)
            {
                // Loop through each modifier in the list and apply them
                foreach (AttributeModifier modifier in Modifiers)
                {
                    switch (modifier.ModifierType)
                    {
                        // Add the modifier to the base value
                        case ModifierType.Additive:
                            tempValue += (BaseValue + modifier.ModifierValue);
                            break;
                        // Multiply the modifier by the base value
                        case ModifierType.Multiplicative:
                            tempValue += (BaseValue * modifier.ModifierValue);
                            break;
                        default:
                        // Do nothing
                        case ModifierType.None:
                            tempValue += 0;
                            break;
                    }
                }

                // Convert the temporary value to a whole number, then set it to the final value
                FinalValue = (int)tempValue;
            }
            else
            {
                // If there are no modifiers, set final value to the base value of the attribute
                FinalValue = BaseValue;
            }
        }
    }
}