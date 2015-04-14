using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI
{
    public class SplashScreen : MonoBehaviour
    {
        public Text Text;
        public Image Panel;

        void Awake()
        {
            Hide();
        }

        public void Show()
        {
            Text.gameObject.SetActive(true);
            Panel.gameObject.SetActive(true);
        }

        public void Hide()
        {
            Text.gameObject.SetActive(false);
            Panel.gameObject.SetActive(false);
        }
    }
}