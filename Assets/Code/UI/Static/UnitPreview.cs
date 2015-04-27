using Assets.Code.Actors;
using Assets.Code.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI.Static
{
    public class UnitPreview : StatWindow
    {
        public Text UnitName;
        public Text HealthPercent;
        public Text AttackValue;
        public Text DefenseValue;
        public Text MovementValue;
        public Image UnitImage;
        public Image HealthBar;
        public Image AttackIcon;
        public Image DefenseIcon;
        public Image MovementIcon;

        void OnEnable()
        {
            Actors.Cursor.OnMouseOverUnit += SetUnitInfo;
            InputHandler.OnInputDisabled += Hide;
        }

        void OnDestroy()
        {
            Actors.Cursor.OnMouseOverUnit -= SetUnitInfo;
            InputHandler.OnInputDisabled -= Hide;
        }

        private void SetUnitInfo(GameObject unitObject)
        {
            if (unitObject != null)
            {
                Unit unit = unitObject.GetComponent<Unit>();
                SpriteRenderer spriteRenderer = unitObject.GetComponent<SpriteRenderer>();
                Sprite sprite = spriteRenderer.sprite;
                Color unitColor;

                if (unit != null && sprite != null)
                {
                    Show();
                    unitColor = spriteRenderer.color;

                    SetText(UnitName, unit.Name);
                    SetImage(UnitImage, sprite);

                    SetStatColorByModifier(AttackValue, AttackIcon, unit.Attack.BaseValue, unit.Attack.ModifiedValue);
                    SetStatColorByModifier(DefenseValue, DefenseIcon, unit.Defense.BaseValue, unit.Defense.ModifiedValue);
                    SetStatColorByModifier(MovementValue, MovementIcon, unit.Movement.BaseValue, unit.Movement.ModifiedValue);

                    SetText(UnitName, unit.Name.ToUpper());
                    SetText(AttackValue, unit.Attack.ModifiedValue.ToString());
                    SetText(DefenseValue, unit.Defense.ModifiedValue.ToString());
                    SetText(MovementValue, unit.Movement.ModifiedValue.ToString());

                    SetColor(UnitImage, unitColor);
                    SetHealthBar(HealthPercent, HealthBar, unit.Health, unit.MaxHealth.ModifiedValue);
                }
                else
                    Hide();
            }
            else
                Hide();
        }

        private void SetHealthBar(Text healthBarPercent, Image healthBar, int healthValue, int totalHealthValue)
        {
            int healthPercent = (int)(((float)healthValue / (float)totalHealthValue) * 100);
            healthBarPercent.text = (healthPercent.ToString() + "%");

            float width = (float)healthPercent;
            float height = healthBar.GetComponent<RectTransform>().rect.height;

            width = Mathf.Clamp(width, 0.0f, 100.0f);
            healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);

            if (healthPercent >= 80)
                SetColor(healthBar, colorVeryHighHealth);
            else if (healthPercent >= 60)
                SetColor(healthBar, colorHighHealth);
            else if (healthPercent >= 40)
                SetColor(healthBar, colorMediumHealth);
            else if (healthPercent >= 20)
                SetColor(healthBar, colorLowHealth);
            else
                SetColor(healthBar, colorVeryLowHealth);
        }

        private void SetStatColorByModifier(Text text, Image icon, int baseStat, int modifiedStat)
        {
            if (baseStat < modifiedStat)
            {
                SetColor(text, colorPositive);
                SetColor(icon, colorPositive);
            }
            else if (baseStat > modifiedStat)
            {
                SetColor(text, colorNegative);
                SetColor(icon, colorNegative);
            }
            else
            {
                SetColor(text, colorNeutral);
                SetColor(icon, colorNeutral);
            }
        }
    }
}