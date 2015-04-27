using Assets.Code.Actors;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI.Static
{
    public class FactionInfo : Window
    {
        public Faction Faction;
        public Text UnitCounter;
        public Text ResourcesCounter;
        public Image Panel;
        private Color panelColor = Color.black;

        void Start()
        {
            panelColor = new Color(
                Faction.UnitColor.r,
                Faction.UnitColor.g,
                Faction.UnitColor.b,
                0.75f);
        }

        void Update()
        {

            SetText(UnitCounter, Faction.Units.Count.ToString());
            SetText(ResourcesCounter, Faction.Resources.ToString());
            SetColor(Panel, panelColor);
        }
    }
}