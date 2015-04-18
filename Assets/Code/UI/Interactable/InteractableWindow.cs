using Assets.Code.UI.Static;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI.Interactable
{
    public abstract class InteractableWindow : Window
    {
        void Awake()
        {
            SetElements();
            AddButtonListeners();

            if (HideOnStart)
                Hide();
        }

        public virtual void AddButtonListeners() { }

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