using Assets.Code.Actors;
using Assets.Code.Events.UnitActions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI.Interactable
{
    public class UnitActionMenu : InteractableWindow
    {
        public delegate void ButtonHandler();
        public static event ButtonHandler OnCaptureClick;
        public static event ButtonHandler OnAttackClick;
        public static event ButtonHandler OnWatiClick;
        public static event ButtonHandler OnCancelClick;

        public Button CaptureButton;
        public Button AttackButton;
        public Button WaitButton;
        public Button CancelButton;

        public Text CaptureButtonText;
        public Text AttackButtonText;
        public Text WaitButtonText;
        public Text CancelButtonText;

        public override void AddButtonListeners()
        {
            DisableButtonWithText(CaptureButton, CaptureButtonText);
            DisableButtonWithText(AttackButton, AttackButtonText);
            EnableButtonWithText(WaitButton, WaitButtonText);
            EnableButtonWithText(CancelButton, CancelButtonText);

            CaptureButton.onClick.AddListener(() =>
            {
                if (OnCaptureClick != null)
                    OnCaptureClick();
            });

            AttackButton.onClick.AddListener(() =>
            {
                if (OnAttackClick != null)
                    OnAttackClick();
            });

            WaitButton.onClick.AddListener(() =>
            {
                if (OnWatiClick != null)
                    OnWatiClick();
            });

            CancelButton.onClick.AddListener(() =>
            {
                if (OnCancelClick != null)
                    OnCancelClick();
            });
        }

        private void SetActionButtonAvailability(Button button, Text buttonText, Unit unit, UnitAction unitAction)
        {
            /*
            if (unit.IsCommandAvailable(commandType))
                EnableButtonWithText(button, buttonText);
            else
                DisableButtonWithText(button, buttonText);
            */
        }
    }
}