using Assets.Code.Controllers.States;
using Assets.Code.Entities.Units;
using Assets.Code.Generic.GUI.Abstract;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.GUI.Screen.Expanded
{
    public class UnitWindow : Window
    {
        public Text UnitName;
        public Text Health;
        public Text HealthRegen;
        public Text Attack;
        public Text AttackRange;
        public Text Defense;
        public Text Movement;
        public Image UnitGraphic;
        public Image FactionBar;
        public Image HealthBar;
        public Image HealthRegenIcon;
        public Image AttackIcon;
        public Image AttackRangeIcon;
        public Image DefenseIcon;
        public Image MovementIcon;

        void OnEnable()
        {
            StartBattleState.OnStateEntry += Hide;
            SelectUnitState.OnUnitSelect += OnWindow;
        }

        void OnDestroy()
        {
            StartBattleState.OnStateEntry -= Hide;
            SelectUnitState.OnUnitSelect -= OnWindow;
        }

        public override void DisplayGUI(GameObject unitObj)
        {
            Unit unit = unitObj.GetComponent<Unit>();
            Sprite sprite = unitObj.GetComponent<SpriteRenderer>().sprite;
            Color unitColor;

            if (unit != null && unit.Faction != null && sprite != null)
            {
                unitColor = unit.Faction.ActiveColor;

                SetStatColorByModifier(HealthRegen, HealthRegenIcon, unit.HealthRegen.BaseValue, unit.HealthRegen.ModifiedValue);
                SetStatColorByModifier(Attack, AttackIcon, unit.Attack.BaseValue, unit.Attack.ModifiedValue);
                SetStatColorByModifier(AttackRange, AttackRangeIcon, unit.AttackRange.BaseValue, unit.AttackRange.ModifiedValue);
                SetStatColorByModifier(Defense, DefenseIcon, unit.Defense.BaseValue, unit.Defense.ModifiedValue);
                SetStatColorByModifier(Movement, MovementIcon, unit.Movement.BaseValue, unit.Movement.ModifiedValue);

                SetText(UnitName, unit.EntityName);
                SetText(Health, "HP " + unit.Health.ToString() + "/" + unit.MaxHealth.ModifiedValue.ToString());
                SetText(HealthRegen, unit.HealthRegen.ModifiedValue.ToString() + " HP Regen");
                SetText(Attack, unit.Attack.ModifiedValue.ToString() + " Attack");
                SetText(AttackRange, unit.AttackRange.ModifiedValue.ToString() + " Attack Range");
                SetText(Defense, unit.Defense.ModifiedValue.ToString() + " Defense");
                SetText(Movement, unit.Movement.ModifiedValue.ToString() + " Movement");

                SetImage(UnitGraphic, sprite);
                SetColor(UnitGraphic, unitColor);
                SetColor(FactionBar, unitColor);
            }
            else
                Hide();
        }

        private void SetStatColorByModifier(Text textElement, Image imageElement, int baseStat, int modifiedStat)
        {
            if (baseStat < modifiedStat)
            {
                SetColor(textElement, colorPositive);
                SetColor(imageElement, colorPositive);
            }
            else if (baseStat > modifiedStat)
            {
                SetColor(textElement, colorNegative);
                SetColor(imageElement, colorNegative);
            }
            else
            {
                SetColor(textElement, colorNeutral);
                SetColor(imageElement, colorNeutral);
            }
        }
    }
}