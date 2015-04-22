using Assets.Code.Actors;
using Assets.Code.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI.Static
{
    public class TurnInfo : Window
    {
        public TurnHandler TurnHandler;
        public Text TurnCounter;
        public Text FactionName;
        public Image FactionColor;

        void Update()
        {
            if (TurnHandler != null && TurnHandler.ActiveFaction != null)
            {
                SetText(TurnCounter, "TURN " + TurnHandler.TurnCount);
                SetText(FactionName, TurnHandler.ActiveFaction.Name.ToUpper());
                SetColor(FactionColor, TurnHandler.ActiveFaction.UnitColor);
            }
        }
    }
}