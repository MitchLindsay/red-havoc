using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI
{
    public class TurnIndicator : MonoBehaviour
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

        public void SetText(string text)
        {
            Text.text = text;
        }

        public void SetPanelColor(Color color)
        {
            Panel.GetComponent<Image>().color = color;
        }
    }
}