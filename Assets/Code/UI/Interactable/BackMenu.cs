using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI.Interactable
{
    public class BackMenu : InteractableWindow
    {
        public delegate void ButtonHandler();
        public static event ButtonHandler OnEndTurnClick;
        public static event ButtonHandler OnCancelClick;

        public Button EndTurnButton;
        public Button CancelButton;

        public override void AddButtonListeners()
        {            
            EndTurnButton.onClick.AddListener(() =>
            {
                if (OnEndTurnClick != null)
                    OnEndTurnClick();
            });

            CancelButton.onClick.AddListener(() =>
            {
                if (OnCancelClick != null)
                    OnCancelClick();
            });
        }
    }
}