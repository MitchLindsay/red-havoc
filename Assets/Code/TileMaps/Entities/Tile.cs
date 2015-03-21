using Assets.Code.GUI.WorldSpace;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.TileMaps.Entities
{
    // Tile.cs - A single tile
    [RequireComponent (typeof(SpriteRenderer))]
    [RequireComponent (typeof(BoxCollider2D))]
    public class Tile : MonoBehaviour
    {
        // Coordinates of the tile, set by the tile map generator
        [HideInInspector]
        public int X = -1;
        [HideInInspector]
        public int Y = -1;

        // Name of the tile type, edited through Unity interface
        public string TileName = "Tile";

        // Attribute modifiers, edited through Unity interface
        public int DefenseBonus = 0;
        public int MovementBonus = 0;
        public int HealthRegenBonus = 0;

        // GUI colors, edited through Unity interface
        public Color GUI_Color_NoBonus;
        public Color GUI_Color_PositiveBonus;
        public Color GUI_Color_NegativeBonus;

        // GUI elements, edited through Unity interface
        public Text GUI_Text_TileName;
        public Text GUI_Text_DefenseBonus;
        public Text GUI_Text_MovementBonus;
        public Text GUI_Text_HealthRegenBonus;
        public Image GUI_Image_DefenseBonus;
        public Image GUI_Image_MovementBonus;
        public Image GUI_Image_HealthRegenBonus;

        // Listen for events when object is created
        void OnEnable()
        {
            MouseCursor.OnMouseOverTile += SetGUIElements;
        }

        // Stop listening for events if object is destroyed
        void OnDestroy()
        {
            MouseCursor.OnMouseOverTile -= SetGUIElements;
        }

        private void SetGUIElements(int x, int y)
        {
            if (x == X && y == Y)
            {
                ChangeGUIText(GUI_Text_TileName, TileName);
                ChangeGUIText(GUI_Text_DefenseBonus, Mathf.Abs(DefenseBonus).ToString());
                ChangeGUIText(GUI_Text_MovementBonus, Mathf.Abs(MovementBonus).ToString());
                ChangeGUIText(GUI_Text_HealthRegenBonus, Mathf.Abs(HealthRegenBonus).ToString());

                ChangeGUIBonusColors(GUI_Text_DefenseBonus, GUI_Image_DefenseBonus, DefenseBonus);
                ChangeGUIBonusColors(GUI_Text_MovementBonus, GUI_Image_MovementBonus, MovementBonus);
                ChangeGUIBonusColors(GUI_Text_HealthRegenBonus, GUI_Image_HealthRegenBonus, HealthRegenBonus);
            }
        }

        // Changes the text in the linked GUI element
        private void ChangeGUIText(Text guiElement, string tileText)
        {
            if (guiElement != null)
                guiElement.text = tileText;
        }

        // Changes the color of the text and image of the linked GUI elements
        private void ChangeGUIBonusColors(Text guiElementText, Image guiElementImage, int bonus)
        {
            if (bonus < 0)
            {
                ChangeGUIColor(guiElementImage, GUI_Color_NegativeBonus);
                ChangeGUIColor(guiElementText, GUI_Color_NegativeBonus);
            }
            else if (bonus > 0)
            {
                ChangeGUIColor(guiElementImage, GUI_Color_PositiveBonus);
                ChangeGUIColor(guiElementText, GUI_Color_PositiveBonus);
            }
            else
            {
                ChangeGUIColor(guiElementImage, GUI_Color_NoBonus);
                ChangeGUIColor(guiElementText, GUI_Color_NoBonus);
            }
        }

        // Changes the color of the linked GUI element
        private void ChangeGUIColor(Text guiElement, Color color)
        {
            if (guiElement != null)
                guiElement.color = color;
        }

        // Changes the color of the linked GUI element
        private void ChangeGUIColor(Image guiElement, Color color)
        {
            if (guiElement != null)
                guiElement.color = color;
        }
    }
}