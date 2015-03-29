using Assets.Code.Entities.Units;
using Assets.Code.Generic.GUI.Abstract;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Entities.GUI.Screen
{
    public class FactionInfo : InfoPanel
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
            if (PanelObject != null)
            {
                if (Faction != null)
                {
                    ShowInfo();

                    SetTextElement(Units, Faction.Units.Count.ToString());
                    SetTextElement(Resources, Faction.Resources.ToString());

                    SetElementColor(UnitsIcon, Faction.FactionColor);
                    SetElementColor(ResourcesIcon, Faction.FactionColor);
                }
                else
                    HideInfo();
            }
        }
    }
}