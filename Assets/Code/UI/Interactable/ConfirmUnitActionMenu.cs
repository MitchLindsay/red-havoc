using Assets.Code.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI.Interactable
{
    public class ConfirmUnitActionMenu : InteractableWindow
    {
        public delegate void ButtonHandler();
        public static event ButtonHandler OnYesClick;
        public static event ButtonHandler OnNoClick;

        public Text MenuText;
        public Button YesButton;
        public Button NoButton;

        void OnEnable()
        {
            UnitActionMenu.OnActionClick += SetMenuText;
        }

        void OnDestroy()
        {
            UnitActionMenu.OnActionClick -= SetMenuText;
        }

        private void SetMenuText(EventID eventID)
        {
            string menuString = "CONFIRM UNAVAILABLE ACTION?";

            switch (eventID)
            {
                case EventID.UnitCapture:
                    menuString = "CONFIRM CAPTURE ACTION?";
                    break;
                case EventID.UnitAttack:
                    menuString = "CONFIRM ATTACK ACTION?";
                    break;
                case EventID.UnitWait:
                default:
                    menuString = "CONFIRM WAIT ACTION?";
                    break;
            }

            SetText(MenuText, menuString);
        }

        public override void AddButtonListeners()
        {
            YesButton.onClick.AddListener(() =>
            {
                if (OnYesClick != null)
                    OnYesClick();
            });

            NoButton.onClick.AddListener(() =>
            {
                if (OnNoClick != null)
                    OnNoClick();
            });
        }
    }
}