using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.GUI.ScreenSpace
{
    public abstract class InfoPanelController : MonoBehaviour
    {
        public GameObject InfoPanel;

        public Color32 ColorStatIncrease = new Color32(61, 229, 118, 255);
        public Color32 ColorStatDecrease = new Color32(229, 61, 61, 255);
        public Color32 ColorStatNeutral = Color.white;

        public virtual void SetInfo() { }
        public virtual void SetInfo(GameObject gameObject) { }

        public void ShowInfo()
        {
            InfoPanel.SetActive(true);
        }

        public void HideInfo()
        {
            InfoPanel.SetActive(false);
        }

        public void HideInfo(GameObject gameObject)
        {
            InfoPanel.SetActive(false);
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