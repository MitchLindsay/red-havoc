using UnityEngine;

namespace Assets.Code.Entities.TileMaps
{
    public class TilePrefabSelector
    {
        public GameObject[] TilePrefabs { get; private set; }

        public TilePrefabSelector()
        {
            SetTilePrefabs();
        }

        private void SetTilePrefabs()
        {
            TilePrefabs = new GameObject[3];

            TilePrefabs[0] = Resources.Load("Tiles/Tile_Plains", typeof(GameObject)) as GameObject;
            TilePrefabs[1] = Resources.Load("Tiles/Tile_Rocks", typeof(GameObject)) as GameObject;
            TilePrefabs[2] = Resources.Load("Tiles/Tile_Mountains", typeof(GameObject)) as GameObject;
        }

        public GameObject GetTilePrefab(float elevation)
        {
            int randomNum = Random.Range(0, 3);
            return TilePrefabs[randomNum];
        }
    }
}