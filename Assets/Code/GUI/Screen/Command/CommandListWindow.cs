using Assets.Code.Controllers.States;
using Assets.Code.Entities.Units;
using Assets.Code.Generic.GUI.Abstract;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.GUI.Screen.Command
{
    public class CommandListWindow : Window
    {
        public delegate void ButtonClickHandler(Unit unit, UnitCommandType commandType);
        public static event ButtonClickHandler OnCommandButtonClick;

        public Button CaptureCommandButton;
        public Button AttackCommandButton;
        public Button WaitCommandButton;
        public Text CaptureCommandText;
        public Text AttackCommandText;
        public Text WaitCommandText;

        void OnEnable()
        {
            StartBattleState.OnStateEntry += Hide;
        }

        void OnDestroy()
        {
            StartBattleState.OnStateEntry -= Hide;
        }

        public override void DisplayGUI(GameObject unitObj)
        {
            Unit unit = unitObj.GetComponent<Unit>();

            if (unit != null && unit.ActiveCommands.Count > 0)
            {
                SetButtonAvailability(CaptureCommandButton, CaptureCommandText, unit, UnitCommandType.Capture);
                SetButtonAvailability(AttackCommandButton, AttackCommandText, unit, UnitCommandType.Attack);
                SetButtonAvailability(WaitCommandButton, WaitCommandText, unit, UnitCommandType.Wait);

                AddButtonListener(CaptureCommandButton, unit, UnitCommandType.Capture);
                AddButtonListener(AttackCommandButton, unit, UnitCommandType.Attack);
                AddButtonListener(WaitCommandButton, unit, UnitCommandType.Wait);
            }
            else
                Hide();
        }

        private void SetButtonAvailability(Button button, Text buttonText, Unit unit, UnitCommandType commandType)
        {
            if (unit.IsCommandAvailable(commandType))
                EnableButtonWithText(button, buttonText);
            else
                DisableButtonWithText(button, buttonText);
        }

        private void AddButtonListener(Button button, Unit unit, UnitCommandType commandType)
        {
            button.onClick.AddListener(() =>
                {
                    if (OnCommandButtonClick != null)
                        OnCommandButtonClick(unit, commandType);
                });
        }
    }
}