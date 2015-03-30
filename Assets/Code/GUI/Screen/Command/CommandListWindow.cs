using Assets.Code.Controllers.States;
using Assets.Code.Entities.Units;
using Assets.Code.Generic.GUI.Abstract;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.GUI.Screen.Command
{
    public class CommandListWindow : Window
    {
        public Text CommandName;
        public Image CommandBackground;

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
            Faction faction;

            if (unit != null)
            {
                faction = unit.Faction;

                if (faction != null)
                {

                }
                else
                    Hide();
            }
            else
                Hide();
        }
    }
}