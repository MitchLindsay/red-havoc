using Assets.Code.Generic;
using Assets.Code.GUI.WorldSpace;
using Assets.Code.TileMaps.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.GUI.ScreenSpace
{
    public class TileInfoGUI : MonoBehaviour
    {
        // Referenced GameObject
        public GameObject Panel_TileInfo;

        // GUI colors, set in Unity interface
        public Color Color_PositiveValue;
        public Color Color_NegativeValue;
        public Color Color_NeutralValue;

        // GUI text, set in Unity interface
        public Text Text_TileInfo_Name;
        public Text Text_TileInfo_DefenseBonus;
        public Text Text_TileInfo_MovementBonus;
        public Text Text_TileInfo_HealthRegenBonus;

        // GUI images, set in Unity interface
        public Image Image_TileInfo_Type;
        public Image Image_TileInfo_DefenseBonus;
        public Image Image_TileInfo_MovementBonus;
        public Image Image_TileInfo_HealthRegenBonus;

        // Listen for events when object is created
        void OnEnable()
        {
            MouseCursor.OnMouseOverTile += SetTileInfo;
        }

        // Stop listening for events if object is destroyed
        void OnDestroy()
        {
            MouseCursor.OnMouseOverTile -= SetTileInfo;
        }

        // Set GUI info
        private void SetTileInfo(GameObject gameObject, int x, int y)
        {
            if (gameObject != null)
            {
                Tile tile = gameObject.GetComponent<Tile>();
                Sprite sprite = gameObject.GetComponent<SpriteRenderer>().sprite;

                if (tile != null && sprite != null)
                {
                    Panel_TileInfo.SetActive(true);

                    SetText(Text_TileInfo_Name, tile.TileName);
                    SetBonusValue(Text_TileInfo_DefenseBonus, Image_TileInfo_DefenseBonus, tile.DefenseBonus);
                    SetBonusValue(Text_TileInfo_MovementBonus, Image_TileInfo_MovementBonus, tile.MovementBonus);
                    SetBonusValue(Text_TileInfo_HealthRegenBonus, Image_TileInfo_HealthRegenBonus, tile.HealthRegenBonus);

                    SetImage(Image_TileInfo_Type, sprite);
                }
                else
                    NullTileInfo();
            }
            else
                NullTileInfo();
        }

        // Set all tile info to null values
        private void NullTileInfo()
        {
            Panel_TileInfo.SetActive(false);

            SetText(Text_TileInfo_Name, "No Tile");
            SetBonusValue(Text_TileInfo_DefenseBonus, Image_TileInfo_DefenseBonus, 0);
            SetBonusValue(Text_TileInfo_MovementBonus, Image_TileInfo_MovementBonus, 0);
            SetBonusValue(Text_TileInfo_HealthRegenBonus, Image_TileInfo_HealthRegenBonus, 0);

            SetImage(Image_TileInfo_Type, null);
        }

        // Set GUI text
        private void SetText(Text textElement, string text)
        {
            if (textElement != null && text != null)
                textElement.text = text;
            else
                textElement.text = "None";
        }

        // Display stat bonus value, with proper formatting and color
        private void SetBonusValue(Text textElement, Image imageElement, int bonus)
        {
            string bonusText;

            if (bonus > 0)
            {
                bonusText = "+" + bonus.ToString();
                SetColor(textElement, Color_PositiveValue);
                SetColor(imageElement, Color_PositiveValue);
            }
            else if (bonus < 0)
            {
                bonusText = "-" + Mathf.Abs(bonus).ToString();
                SetColor(textElement, Color_NegativeValue);
                SetColor(imageElement, Color_NegativeValue);
            }
            else
            {
                bonusText = "0";
                SetColor(textElement, Color_NeutralValue);
                SetColor(imageElement, Color_NeutralValue);
            }

            SetText(textElement, bonusText);
        }

        // Set GUI image
        private void SetImage(Image imageElement, Sprite image)
        {
            if (imageElement != null && image != null)
                imageElement.sprite = image;
            else
                imageElement.sprite = null;
        }

        // Set GUI element color
        private void SetColor(Text textElement, Color color)
        {
            if (textElement != null)
                textElement.color = color;
        }

        // Set GUI element color
        private void SetColor(Image imageElement, Color color)
        {
            if (imageElement != null)
                imageElement.color = color;
        }
    }
}