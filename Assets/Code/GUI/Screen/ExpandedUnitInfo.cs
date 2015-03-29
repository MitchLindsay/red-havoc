using Assets.Code.Controllers.States;
using Assets.Code.Entities.Units;
using Assets.Code.Generic.GUI.Abstract;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Entities.GUI.Screen
{
    public class ExpandedUnitInfo : InfoPanel
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
            StartBattleState.OnStateEntry += HideInfo;
            SelectUnitState.OnUnitSelect += SetInfo;
            SelectUnitCommandState.OnUnitDeselect += HideInfo;
        }

        void OnDestroy()
        {
            StartBattleState.OnStateEntry -= HideInfo;
            SelectUnitState.OnUnitSelect -= SetInfo;
            SelectUnitCommandState.OnUnitDeselect -= HideInfo;
        }

        public override void SetInfo(GameObject unitGameObject)
        {
            if (PanelObject != null)
            {
                if (unitGameObject != null)
                {
                    Unit unit = unitGameObject.GetComponent<Unit>();
                    Sprite sprite = unitGameObject.GetComponent<SpriteRenderer>().sprite;
                    Color factionColor = unitGameObject.GetComponent<SpriteRenderer>().color;

                    if (unit != null && sprite != null)
                    {
                        ShowInfo();

                        SetStatColorByModifier(HealthRegen, HealthRegenIcon, unit.HealthRegen.BaseValue, unit.HealthRegen.ModifiedValue);
                        SetStatColorByModifier(Attack, AttackIcon, unit.Attack.BaseValue, unit.Attack.ModifiedValue);
                        SetStatColorByModifier(AttackRange, AttackRangeIcon, unit.AttackRange.BaseValue, unit.AttackRange.ModifiedValue);
                        SetStatColorByModifier(Defense, DefenseIcon, unit.Defense.BaseValue, unit.Defense.ModifiedValue);
                        SetStatColorByModifier(Movement, MovementIcon, unit.Movement.BaseValue, unit.Movement.ModifiedValue);

                        SetTextElement(UnitName, unit.EntityName);
                        SetTextElement(Health, "HP " + unit.Health.ToString() + "/" + unit.MaxHealth.ModifiedValue.ToString());
                        SetTextElement(HealthRegen, unit.HealthRegen.ModifiedValue.ToString() + " HP  Regen");
                        SetTextElement(Attack, unit.Attack.ModifiedValue.ToString() + " Attack");
                        SetTextElement(AttackRange, unit.AttackRange.ModifiedValue.ToString() + " Attack Range");
                        SetTextElement(Defense, unit.Defense.ModifiedValue.ToString() + " Defense");
                        SetTextElement(Movement, unit.Movement.ModifiedValue.ToString() + " Movement");

                        SetImageElement(UnitGraphic, sprite);
                        SetElementColor(UnitGraphic, factionColor);
                        SetElementColor(FactionBar, factionColor);
                    }
                    else
                        HideInfo();
                }
                else
                   HideInfo();
            }
        }

        private void SetStatColorByModifier(Text textElement, Image imageElement, int baseStat, int modifiedStat)
        {
            if (baseStat < modifiedStat)
            {
                SetElementColor(textElement, ColorStatIncrease);
                SetElementColor(imageElement, ColorStatIncrease);
            }
            else if (baseStat > modifiedStat)
            {
                SetElementColor(textElement, ColorStatDecrease);
                SetElementColor(imageElement, ColorStatDecrease);
            }
            else
            {
                SetElementColor(textElement, ColorStatNeutral);
                SetElementColor(imageElement, ColorStatNeutral);
            }
        }
    }
}