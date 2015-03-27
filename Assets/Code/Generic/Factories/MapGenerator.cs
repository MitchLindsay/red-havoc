using Assets.Code.Generic.GameObjects;
using UnityEngine;

namespace Assets.Code.Generic.Factories
{
    public abstract class MapGenerator<T> : MonoBehaviour
    {
        public GameObject MapContainer = null;
        public Vector3 MapLocation = Vector3.zero;
        public string DataTag = "Data";
        public string MapTag = "Map";

        public string MapName = "Generic Map";
        public int Width = 10;
        public int Height = 10;

        public virtual void DestroyMap()
        {
            GameObject[] dataObjects = GameObject.FindGameObjectsWithTag(DataTag);
            GameObject[] mapObjects = GameObject.FindGameObjectsWithTag(MapTag);

            foreach (GameObject dataObject in dataObjects)
                DestroyImmediate(dataObject);

            foreach (GameObject mapObject in mapObjects)
                DestroyImmediate(mapObject);
        }
    }
}