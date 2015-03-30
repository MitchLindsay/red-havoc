using Assets.Code.Entities.Units;
using Assets.Code.Generic.GUI.Abstract;
using Assets.Code.GUI.World;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.GUI.Screen.Preview
{
    public class UnitPreviewWindow : Window
    {
        public Text UnitName;
        public Text Health;
        public Text Attack;
        public Text Defense;
        public Text Movement;
        public Image UnitGraphic;
        public Image HealthBar;
        public Image AttackIcon;
        public Image DefenseIcon;
        public Image MovementIcon;

        void OnEnable()
        {
            MouseCursor.OnMouseOverUnit += OnWindow;
        }

        void OnDestroy()
        {
            MouseCursor.OnMouseOverUnit -= OnWindow;
        }

        public override void DisplayGUI(GameObject unitObj)
        {
            Unit unit = unitObj.GetComponent<Unit>();
            Sprite sprite = unitObj.GetComponent<SpriteRenderer>().sprite;
            Color unitColor;

            if (unit != null && unit.Faction != null && sprite != null)
            { 
                unitColor = unit.Faction.ActiveColor;

                SetText(UnitName, unit.EntityName);
                SetImage(UnitGraphic, sprite);

                SetStatColorByModifier(Attack, unit.Attack.BaseValue, unit.Attack.ModifiedValue);
                SetStatColorByModifier(Defense, unit.Defense.BaseValue, unit.Defense.ModifiedValue);
                SetStatColorByModifier(Movement, unit.Movement.BaseValue, unit.Movement.ModifiedValue);

                SetText(UnitName, unit.EntityName);
                SetText(Health, unit.Health.ToString() + "/" + unit.MaxHealth.ModifiedValue.ToString() + " HP");
                SetText(Attack, unit.Attack.ModifiedValue.ToString());
                SetText(Defense, unit.Defense.ModifiedValue.ToString());
                SetText(Movement, unit.Movement.ModifiedValue.ToString());

                SetColor(UnitGraphic, unitColor);
                SetColor(HealthBar, unitColor);
                SetColor(AttackIcon, unitColor);
                SetColor(DefenseIcon, unitColor);
                SetColor(MovementIcon, unitColor);
            }
            else
                Hide();
        }

        private void SetStatColorByModifier(Text textElement, int baseStat, int modifiedStat)
        {
            if (baseStat < modifiedStat)
                SetColor(textElement, colorPositive);
            else if (baseStat > modifiedStat)
                SetColor(textElement, colorNegative);
            else
                SetColor(textElement, colorNeutral);
        }
    }
}