using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.TileMaps.Entities
{
    // Tile.cs - A single tile
    [RequireComponent (typeof(SpriteRenderer))]
    public class Tile : MonoBehaviour
    {
        // Unique identifier for the tile, assigned by the tile map generator
        [HideInInspector]
        public int TileID = -1;

        // Name of the tile type, edited through Unity interface
        public string TileName = "Tile";
    }
}