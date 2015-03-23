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
        // Name of the tile type, edited through Unity interface
        public string TileName = "Tile";

        // Attribute modifiers, edited through Unity interface
        public int DefenseBonus = 0;
        public int MovementBonus = 0;
        public int HealthRegenBonus = 0;
    }
}