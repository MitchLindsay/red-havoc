using UnityEngine;

namespace Assets.Code.TileMaps.Generators
{
    // TilePrefabSelector.cs - Stores all of the tile prefab data, chooses a tile given an elevation
    public class TilePrefabSelector
    {
        // Array containing the tile prefabs
        public GameObject[] TilePrefabs { get; private set; }

        // Constructor
        public TilePrefabSelector()
        {
            SetTilePrefabs();
        }

        // Set array of tile prefabs
        private void SetTilePrefabs()
        {
            TilePrefabs = new GameObject[3];

            TilePrefabs[0] = Resources.Load("Tiles/Tile_Plains", typeof(GameObject)) as GameObject;
            TilePrefabs[1] = Resources.Load("Tiles/Tile_Rocks", typeof(GameObject)) as GameObject;
            TilePrefabs[2] = Resources.Load("Tiles/Tile_Mountains", typeof(GameObject)) as GameObject;
        }

        // Return a tile prefab based on the data given
        public GameObject GetTilePrefab(float elevation)
        {
            return TilePrefabs[0];
        }
    }
}