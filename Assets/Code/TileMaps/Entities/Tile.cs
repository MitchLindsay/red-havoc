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

        // GUI label for the tile name
        public Text TileGUIText;
        // GUI image for the tile graphic
        public Image TileGUIImage;

        void OnMouseOver()
        {
            if (TileGUIText != null)
                TileGUIText.text = "Tile: " + TileName;

            if (TileGUIImage != null && TileGraphic != null)
                TileGUIImage.sprite = TileGraphic;
        }
    }
}