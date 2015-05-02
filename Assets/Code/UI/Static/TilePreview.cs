using Assets.Code.Actors;
using Assets.Code.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI.Static
{
    public class TilePreview : StatWindow
    {
        public Text TileName;
        public Text MovementCost;
        public Text DefenseBonus;
        public Text HealthRegenBonus;
        public Image TileImage;
        public Image MovementIcon;
        public Image DefenseIcon;
        public Image HealthIcon;

        void OnEnable()
        {
            Actors.Cursor.OnMouseOverTile += SetTileInfo;
            InputHandler.OnCursorDisabled += Hide;
        }

        void OnDestroy()
        {
            Actors.Cursor.OnMouseOverTile -= SetTileInfo;
            InputHandler.OnCursorDisabled -= Hide;
        }

        private void SetTileInfo(GameObject tileObject)
        {
            if (tileObject != null)
            {
                Tile tile = tileObject.GetComponent<Tile>();
                Sprite sprite = tileObject.GetComponent<SpriteRenderer>().sprite;

                if (tile != null && sprite != null)
                {
                    Show();

                    SetText(TileName, tile.Name.ToUpper());
                    SetImage(TileImage, sprite);

                    SetStat(MovementCost, MovementIcon, -tile.MovementCost);
                    SetStat(DefenseBonus, DefenseIcon, tile.DefenseBonus);
                    SetStat(HealthRegenBonus, HealthIcon, tile.HealthRegenBonus);
                }
                else
                    Hide();
            }
            else
                Hide();
        }

        private void SetStat(Text text, Image icon, int statValue)
        {
            string statText = "";

            if (statValue > 0)
            {
                statText = "+" + statValue.ToString();
                SetColor(text, colorPositive);
                SetColor(icon, colorPositive);
            }
            else if (statValue < 0)
            {
                statText = statValue.ToString();
                SetColor(text, colorNegative);
                SetColor(icon, colorNegative);
            }
            else
            {
                statText = "0";

                SetColor(text, colorNeutral);
                SetColor(icon, colorNeutral);
            }

            SetText(text, statText);
        }
    }
}