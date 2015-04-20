using Assets.Code.Actors;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI.Static
{
    public class UnitPreview : StatWindow
    {
        public Text UnitName;
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
        }

        void OnDestroy()
        {
            Actors.Cursor.OnMouseOverUnit -= SetUnitInfo;
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
                }
                else
                    Hide();
            }
            else
                Hide();
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