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
        // Sprite of the tile, edited through Unity interface
        public Sprite TileGraphic = null;

        // GUI elements, edited through Unity interface
        public Text TileTypeGUIText;
        public Image TileTypeGUIImage;

        void OnMouseOver()
        {
            if (TileTypeGUIText != null)
                TileTypeGUIText.text = TileName;

            if (TileTypeGUIImage != null && TileGraphic != null)
                TileTypeGUIImage.sprite = TileGraphic;
        }
    }
}