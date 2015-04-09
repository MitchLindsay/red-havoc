using Assets.Code.Controllers.StateMachine.States;
using Assets.Code.Entities.Units;
using Assets.Code.Generic.GUI.Abstract;
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

        private Unit selectedUnit = null;

        void OnEnable()
        {
            StartBattleState.OnStateEntry += Hide;
            SelectUnitState.OnUnitSelect += SetSelectedUnit;
            SelectUnitCommandState.OnStateEntry += OnWindow;
            SelectUnitCommandState.OnStateExit += Hide;
        }

        void OnDestroy()
        {
            StartBattleState.OnStateEntry -= Hide;
            SelectUnitState.OnUnitSelect -= SetSelectedUnit;
            SelectUnitCommandState.OnStateEntry -= OnWindow;
            SelectUnitCommandState.OnStateExit -= Hide;
        }

        public override void DisplayGUI()
        {
            if (selectedUnit != null && selectedUnit.IsActive)
            {
                SetButtonAvailability(CaptureCommandButton, CaptureCommandText, selectedUnit, UnitCommandType.Capture);
                SetButtonAvailability(AttackCommandButton, AttackCommandText, selectedUnit, UnitCommandType.Attack);
                SetButtonAvailability(WaitCommandButton, WaitCommandText, selectedUnit, UnitCommandType.Wait);

                AddButtonListener(CaptureCommandButton, selectedUnit, UnitCommandType.Capture);
                AddButtonListener(AttackCommandButton, selectedUnit, UnitCommandType.Attack);
                AddButtonListener(WaitCommandButton, selectedUnit, UnitCommandType.Wait);
            }
            else
                Hide();
        }

        private void SetSelectedUnit(GameObject unitObj)
        {
            if (unitObj != null)
                selectedUnit = unitObj.GetComponent<Unit>();
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