using UnityEngine;

namespace Assets.Code.GUI.ScreenSpace
{
    public class FollowMouse : MonoBehaviour
    {
        void Update()
        {
            transform.position = Input.mousePosition;
        }
    }
}