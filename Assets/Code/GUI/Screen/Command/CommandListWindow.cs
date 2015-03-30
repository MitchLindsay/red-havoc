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
        private float commandBackgroundSize = 28.0f;
        private RectTransform windowRectTransform;

        public Text CaptureCommandName;
        public Text AttackCommandName;
        public Text MoveCommandName;
        public Text WaitCommandName;
        public Image CaptureCommandBackground;
        public Image AttackCommandBackground;
        public Image MoveCommandBackground;
        public Image WaitCommandBackground;

        void OnEnable()
        {
            StartBattleState.OnStateEntry += Hide;
            SelectUnitState.OnUnitSelect += OnWindow;
            SelectUnitCommandState.OnUnitDeselect += Hide;
        }

        void OnDestroy()
        {
            StartBattleState.OnStateEntry -= Hide;
            SelectUnitState.OnUnitSelect -= OnWindow;
            SelectUnitCommandState.OnUnitDeselect -= Hide;
        }

        public override void DisplayGUI(GameObject unitObj)
        {
            Unit unit = unitObj.GetComponent<Unit>();

            if (unit != null && unit.ActiveCommands.Count > 0)
            {
                if (unit.IsCommandAvailable(UnitCommandType.Capture))
                    ShowCommand(CaptureCommandName, CaptureCommandBackground);
                else
                    HideCommand(CaptureCommandName, CaptureCommandBackground);

                if (unit.IsCommandAvailable(UnitCommandType.Attack))
                    ShowCommand(AttackCommandName, AttackCommandBackground);
                else
                    HideCommand(AttackCommandName, AttackCommandBackground);

                if (unit.IsCommandAvailable(UnitCommandType.Move))
                    ShowCommand(MoveCommandName, MoveCommandBackground);
                else
                    HideCommand(MoveCommandName, MoveCommandBackground);

                if (unit.IsCommandAvailable(UnitCommandType.Wait))
                    ShowCommand(WaitCommandName, WaitCommandBackground);
                else
                    HideCommand(WaitCommandName, WaitCommandBackground);
            }
            else
                Hide();
        }

        private void ShowCommand(Text commandText, Image commandBackground)
        {
            commandText.gameObject.SetActive(true);
            commandBackground.gameObject.SetActive(true);
        }

        private void HideCommand(Text commandText, Image commandBackground)
        {
            commandText.gameObject.SetActive(false);
            commandBackground.gameObject.SetActive(false);
        }
    }
}