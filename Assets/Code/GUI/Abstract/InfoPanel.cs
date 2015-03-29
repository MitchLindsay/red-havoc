using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Generic.GUI.Abstract
{
    public abstract class InfoPanel : MonoBehaviour
    {
        public GameObject PanelObject;

        public Color32 ColorStatIncrease = new Color32(61, 229, 118, 255);
        public Color32 ColorStatDecrease = new Color32(229, 61, 61, 255);
        public Color32 ColorStatNeutral = Color.white;

        public virtual void SetInfo() { }
        public virtual void SetInfo(GameObject gameObject) { }

        public void ShowInfo()
        {
            PanelObject.SetActive(true);
        }

        public void HideInfo()
        {
            PanelObject.SetActive(false);
        }

        public void HideInfo(GameObject gameObject)
        {
            PanelObject.SetActive(false);
        }

        public void SetTextElement(Text element, string info)
        {
            if (element != null && info != null)
                element.text = info;
        }

        public void SetImageElement(Image element, Sprite graphic)
        {
            if (element != null && graphic != null)
                element.sprite = graphic;
        }

        public void SetElementColor(Text element, Color color)
        {
            if (element != null)
                element.color = color;
        }

        public void SetElementColor(Image element, Color color)
        {
            if (element != null)
                element.color = color;
        }
    }
}