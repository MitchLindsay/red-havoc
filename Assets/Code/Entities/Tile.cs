using UnityEngine;

namespace Assets.Code.Entities
{
    [RequireComponent (typeof(SpriteRenderer))]
    public class Tile : MonoBehaviour
    {
        /* Tile ID (Primary Key) - assigned by tile map generator
         * Tile Name - name of tile type (grass, forest, etc)
         * Movement Cost - based on 'movement type' (foot, tires, etc)
         * Special Effects - special effect for unit standing on tile (mountains - extra vision, buildings - captureable, etc)
         */

        // Tile ID - Assigned by Tile Map Generator
        [HideInInspector]
        public int TileID = -1;
    }
}