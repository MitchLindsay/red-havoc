using Assets.Code.Entities.Units;
using Assets.Code.Generic.GUI.Abstract;
using UnityEngine.UI;

namespace Assets.Code.GUI.Screen.Static
{
    public class FactionWindow : Window
    {
        public Faction Faction;
        public Text Units;
        public Text Resources;
        public Image UnitsIcon;
        public Image ResourcesIcon;

        void Update()
        {
            OnWindow();
        }

        public override void DisplayGUI()
        {
            SetText(Units, Faction.Units.Count.ToString());
            SetText(Resources, Faction.Resources.ToString());
            SetColor(UnitsIcon, Faction.ActiveColor);
            SetColor(ResourcesIcon, Faction.ActiveColor);
        }
    }
}