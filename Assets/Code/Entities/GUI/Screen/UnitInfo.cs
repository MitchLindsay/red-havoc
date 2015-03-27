using Assets.Code.Entities.GUI.World;
using Assets.Code.Entities.Units;
using Assets.Code.Generic.GUI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Entities.GUI.Screen
{
    public class UnitInfo : InfoPanel
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
            MouseCursor.OnMouseOverUnit += SetInfo;
        }

        void OnDestroy()
        {
            MouseCursor.OnMouseOverUnit -= SetInfo;
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

                        SetStatColorByModifier(Attack, unit.Attack.BaseValue, unit.Attack.ModifiedValue);
                        SetStatColorByModifier(Defense, unit.Defense.BaseValue, unit.Defense.ModifiedValue);
                        SetStatColorByModifier(Movement, unit.Movement.BaseValue, unit.Movement.ModifiedValue);

                        SetTextElement(UnitName, unit.EntityName);
                        SetTextElement(Health, unit.Health.ToString() + "/" + unit.MaxHealth.ModifiedValue.ToString() + " HP");
                        SetTextElement(Attack, unit.Attack.ModifiedValue.ToString());
                        SetTextElement(Defense, unit.Defense.ModifiedValue.ToString());
                        SetTextElement(Movement, unit.Movement.ModifiedValue.ToString());

                        SetImageElement(UnitGraphic, sprite);
                        SetElementColor(UnitGraphic, factionColor);
                        SetElementColor(HealthBar, factionColor);
                        SetElementColor(AttackIcon, factionColor);
                        SetElementColor(DefenseIcon, factionColor);
                        SetElementColor(MovementIcon, factionColor);
                    }
                    else
                        HideInfo();
                }
                else
                    HideInfo();
            }
        }

        private void SetStatColorByModifier(Text textElement, int baseStat, int modifiedStat)
        {
            if (baseStat < modifiedStat)
                SetElementColor(textElement, ColorStatIncrease);
            else if (baseStat > modifiedStat)
                SetElementColor(textElement, ColorStatDecrease);
            else
                SetElementColor(textElement, ColorStatNeutral);
        }
    }
}