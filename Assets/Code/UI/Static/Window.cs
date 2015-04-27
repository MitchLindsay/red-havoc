using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI.Static
{
    public abstract class Window : MonoBehaviour
    {
        public bool HideOnStart = true;
        private List<GameObject> elements;

        void Awake()
        {
            SetElements();

            if (HideOnStart)
                Hide();
        }

        protected void SetElements()
        {
            elements = new List<GameObject>();

            Transform parentTransform = gameObject.transform;
            foreach (Transform childTransform in parentTransform)
                elements.Add(childTransform.gameObject);
        }

        public void Show()
        {
            if (elements != null && elements.Count > 0)
            {
                foreach (GameObject element in elements)
                    element.SetActive(true);
            }
        }

        public void Hide()
        {
            if (elements != null && elements.Count > 0)
                foreach (GameObject element in elements)
                    element.SetActive(false);
        }

        public void SetText(Text element, string info)
        {
            if (element != null && info != null)
                element.text = info;
        }

        public void SetImage(Image element, Sprite graphic)
        {
            if (element != null && graphic != null)
                element.sprite = graphic;
        }

        public void SetColor(Text element, Color color)
        {
            if (element != null)
                element.color = color;
        }

        public void SetColor(Image element, Color color)
        {
            if (element != null)
                element.color = color;
        }
    }
}