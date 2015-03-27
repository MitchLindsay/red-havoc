using System.Collections.Generic;

namespace Assets.Code.Entities.Stats
{
    public class Stat
    {
        public string StatName { get; private set; }
        public int BaseValue { get; private set; }
        public int ModifiedValue { get; private set; }
        public List<StatModifier> Modifiers { get; private set; }

        public Stat(string StatName, int baseValue)
        {
            this.StatName = StatName;
            this.BaseValue = baseValue;
            this.ModifiedValue = baseValue;
            this.Modifiers = new List<StatModifier>();
        }

        public void AddModifier(StatModifier modifier)
        {
            if (!Modifiers.Contains(modifier))
            {
                Modifiers.Add(modifier);
                CalculateModifiedValue();
            }
        }

        public void RemoveModifier(StatModifier modifier)
        {
            if (Modifiers.Contains(modifier))
            {
                Modifiers.Remove(modifier);
                CalculateModifiedValue();
            }
        }

        public void RemoveAllModifiers()
        {
            Modifiers = new List<StatModifier>();
            CalculateModifiedValue();
        }

        private void CalculateModifiedValue()
        {
            float tempValue = BaseValue;

            foreach (StatModifier modifier in Modifiers)
            {
                switch (modifier.ModifierType)
                {
                    case StatModifierType.Additive:
                        tempValue += modifier.ModifierValue;
                        break;
                    case StatModifierType.Multiplicative:
                        tempValue *= modifier.ModifierValue;
                        break;
                    case StatModifierType.None:
                    default:
                        break;
                }
            }

            ModifiedValue = (int)tempValue;
        }
    }
}