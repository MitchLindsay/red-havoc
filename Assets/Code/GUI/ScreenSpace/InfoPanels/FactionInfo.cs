using Assets.Code.Units.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.GUI.ScreenSpace
{
    public class FactionInfo : InfoPanelController
    {
        public Faction Faction;

        public Text Units;
        public Text Resources;

        public Image UnitsIcon;
        public Image ResourcesIcon;

        void Update()
        {
            SetInfo();
        }

        public override void SetInfo()
        {
            if (InfoPanel != null)
            {
                if (Faction != null)
                {
                    InfoPanel.SetActive(true);

                    SetTextElement(Units, Faction.Units.Count.ToString());
                    SetTextElement(Resources, Faction.Resources.ToString());

                    SetElementColor(UnitsIcon, Faction.FactionColor);
                    SetElementColor(ResourcesIcon, Faction.FactionColor);
                }
                else
                    InfoPanel.SetActive(false);
            }
        }
    }
}