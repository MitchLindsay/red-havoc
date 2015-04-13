using UnityEngine;

namespace Assets.Code.Generic
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance = null;
        public static T Instance
        {
            get
            {
                if (!instance)
                {
                    instance = (T)FindObjectOfType(typeof(T)) as T;

                    if (!instance)
                    {
                        GameObject singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = typeof(T).ToString();

                        DontDestroyOnLoad(singleton);
                    }
                }

                return instance;
            }
        }

        void OnApplicationQuit()
        {
            instance = null;
        }
    }
}