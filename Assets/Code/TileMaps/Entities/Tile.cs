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
        // Unique identifier for the tile, assigned by the tile map generator
        [HideInInspector]
        public int TileID = -1;

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

        void OnMouseOver()
        {
            if (GUI_Text_TileName != null)
                GUI_Text_TileName.text = TileName;

            if (GUI_Text_DefenseBonus != null)
                GUI_Text_DefenseBonus.text = Mathf.Abs(DefenseBonus).ToString();

            if (GUI_Text_MovementBonus != null)
                GUI_Text_MovementBonus.text = Mathf.Abs(MovementBonus).ToString();

            if (GUI_Text_HealthRegenBonus != null)
                GUI_Text_HealthRegenBonus.text = Mathf.Abs(HealthRegenBonus).ToString();

            if (DefenseBonus < 0)
            {
                if (GUI_Image_DefenseBonus != null)
                    GUI_Image_DefenseBonus.color = GUI_Color_NegativeBonus;

                if (GUI_Text_DefenseBonus != null)
                    GUI_Text_DefenseBonus.color = GUI_Color_NegativeBonus;
            }
            else if (DefenseBonus > 0)
            {
                if (GUI_Image_DefenseBonus != null)
                    GUI_Image_DefenseBonus.color = GUI_Color_PositiveBonus;

                if (GUI_Text_DefenseBonus != null)
                    GUI_Text_DefenseBonus.color = GUI_Color_PositiveBonus;
            }
            else
            {
                if (GUI_Image_DefenseBonus != null)
                    GUI_Image_DefenseBonus.color = GUI_Color_NoBonus;

                if (GUI_Text_DefenseBonus != null)
                    GUI_Text_DefenseBonus.color = GUI_Color_NoBonus;
            }

            if (MovementBonus < 0)
            {
                if (GUI_Image_MovementBonus != null)
                    GUI_Image_MovementBonus.color = GUI_Color_NegativeBonus;

                if (GUI_Text_MovementBonus != null)
                    GUI_Text_MovementBonus.color = GUI_Color_NegativeBonus;
            }
            else if (MovementBonus > 0)
            {
                if (GUI_Image_MovementBonus != null)
                    GUI_Image_MovementBonus.color = GUI_Color_PositiveBonus;

                if (GUI_Text_MovementBonus != null)
                    GUI_Text_MovementBonus.color = GUI_Color_PositiveBonus;
            }
            else
            {
                if (GUI_Image_MovementBonus != null)
                    GUI_Image_MovementBonus.color = GUI_Color_NoBonus;

                if (GUI_Text_MovementBonus != null)
                    GUI_Text_MovementBonus.color = GUI_Color_NoBonus;
            }

            if (HealthRegenBonus < 0)
            {
                if (GUI_Image_HealthRegenBonus != null)
                    GUI_Image_HealthRegenBonus.color = GUI_Color_NegativeBonus;

                if (GUI_Text_HealthRegenBonus != null)
                    GUI_Text_HealthRegenBonus.color = GUI_Color_NegativeBonus;
            }
            else if (HealthRegenBonus > 0)
            {
                if (GUI_Image_HealthRegenBonus != null)
                    GUI_Image_HealthRegenBonus.color = GUI_Color_PositiveBonus;

                if (GUI_Text_HealthRegenBonus != null)
                    GUI_Text_HealthRegenBonus.color = GUI_Color_PositiveBonus;
            }
            else
            {
                if (GUI_Image_HealthRegenBonus != null)
                    GUI_Image_HealthRegenBonus.color = GUI_Color_NoBonus;

                if (GUI_Text_HealthRegenBonus != null)
                    GUI_Text_HealthRegenBonus.color = GUI_Color_NoBonus;
            }
        }
    }
}