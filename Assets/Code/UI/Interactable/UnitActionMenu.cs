using Assets.Code.Actors;
using Assets.Code.Events;
using Assets.Code.Events.UnitActions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI.Interactable
{
    public class UnitActionMenu : InteractableWindow
    {
        public delegate void ActionButtonHandler(EventID eventID);
        public delegate void CancelButtonHandler();
        public static event ActionButtonHandler OnActionClick;
        public static event CancelButtonHandler OnCancelClick;

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
                if (OnActionClick != null)
                    OnActionClick(EventID.UnitCapture);
            });

            AttackButton.onClick.AddListener(() =>
            {
                if (OnActionClick != null)
                    OnActionClick(EventID.UnitAttack);
            });

            WaitButton.onClick.AddListener(() =>
            {
                if (OnActionClick != null)
                    OnActionClick(EventID.UnitWait);
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