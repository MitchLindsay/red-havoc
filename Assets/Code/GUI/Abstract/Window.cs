using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Generic.GUI.Abstract
{
    public abstract class Window : MonoBehaviour
    {
        public GameObject WindowObj;

        protected Color32 colorPositive = new Color32(61, 229, 118, 255);
        protected Color32 colorNegative = new Color32(229, 61, 61, 255);
        protected Color32 colorNeutral = Color.white;

        public void OnWindow()
        {
            if (WindowObj != null)
            {
                Show();
                DisplayGUI();
            }
            else
                Hide();
        }

        public void OnWindow(GameObject referenceObj)
        {
            if (WindowObj != null && referenceObj != null)
            {
                Show();
                DisplayGUI(referenceObj);
            }
            else
                Hide();
        }

        public virtual void DisplayGUI() { }
        public virtual void DisplayGUI(GameObject referenceObj) { }

        public void Show()
        {
            WindowObj.SetActive(true);
        }

        public void Hide()
        {
            WindowObj.SetActive(false);
        }

        public void Hide(GameObject gameObject)
        {
            WindowObj.SetActive(false);
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

        public void EnableButton(Button button)
        {
            button.interactable = true;
        }

        public void DisableButton(Button button)
        {
            button.interactable = false;
        }

        public void EnableButtonWithText(Button button, Text text)
        {
            EnableButton(button);
            SetColor(text, Color.white);
        }

        public void DisableButtonWithText(Button button, Text text)
        {
            DisableButton(button);
            SetColor(text, new Color(1.0f, 1.0f, 1.0f, 0.5f));
        }
    }
}