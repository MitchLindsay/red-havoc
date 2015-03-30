using Assets.Code.Entities.Tiles;
using Assets.Code.Generic.GUI.Abstract;
using Assets.Code.GUI.World;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.GUI.Screen.Preview
{
    public class TilePreviewWindow : Window
    {
        public Text TileName;
        public Text HealthRegenBonus;
        public Text DefenseBonus;
        public Text MovementBonus;
        public Image TileGraphic;
        public Image HealthRegenBonusIcon;
        public Image DefenseBonusIcon;
        public Image MovementBonusIcon;

        void OnEnable()
        {
            MouseCursor.OnMouseOverTile += OnWindow;
        }

        void OnDestroy()
        {
            MouseCursor.OnMouseOverTile -= OnWindow;
        }

        public override void DisplayGUI(GameObject tileObj)
        {
            Tile tile = tileObj.GetComponent<Tile>();
            Sprite sprite = tileObj.GetComponent<SpriteRenderer>().sprite;

            if (tile != null && sprite != null)
            {
                SetText(TileName, tile.EntityName);
                SetImage(TileGraphic, sprite);

                SetStatBonus(HealthRegenBonus, HealthRegenBonusIcon, tile.HealthRegenBonus);
                SetStatBonus(DefenseBonus, DefenseBonusIcon, tile.DefenseBonus);
                SetStatBonus(MovementBonus, MovementBonusIcon, tile.MovementBonus);
            }
            else
                Hide();
        }

        private void SetStatBonus(Text textElement, Image imageElement, int statBonusValue)
        {
            string statBonusText = "";

            if (statBonusValue > 0)
            {
                statBonusText = "+" + statBonusValue.ToString();
                SetColor(textElement, colorPositive);
                SetColor(imageElement, colorPositive);
            }
            else if (statBonusValue < 0)
            {
                statBonusText = statBonusValue.ToString();
                SetColor(textElement, colorNegative);
                SetColor(imageElement, colorNegative);
            }
            else
            {
                statBonusText = "0";

                SetColor(textElement, colorNeutral);
                SetColor(imageElement, colorNeutral);
            }

            SetText(textElement, statBonusText);
        }
    }
}