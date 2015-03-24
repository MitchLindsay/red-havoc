using Assets.Code.Units.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.GUI.ScreenSpace
{
    public class PlayerInfoGUI : MonoBehaviour
    {
        // Referenced GameObjects
        public GameObject Panel_PlayerInfo;
        public Faction Faction_Player;

        // GUI text, set in Unity interface
        public Text Text_PlayerInfo_Units;
        public Text Text_PlayerInfo_Resources;

        // GUI images, set in Unity interface
        public Image Image_PlayerInfo_Units;
        public Image Image_PlayerInfo_Resources;

        // Listen for events when object is created
        void OnEnable()
        {
            Faction.OnInitialUnitAdditionComplete += SetGUIElements;
        }

        // Stop listening for events if object is destroyed
        void OnDestroy()
        {
            Faction.OnInitialUnitAdditionComplete -= SetGUIElements;
        }

        private void SetGUIElements()
        {
            SetColor();
            SetUnits();
            SetResources();
        }

        // Set image colors to the faction color
        private void SetColor()
        {
            if (Image_PlayerInfo_Units != null && Image_PlayerInfo_Resources != null)
            {
                Image_PlayerInfo_Units.color = Faction_Player.FactionColor;
                Image_PlayerInfo_Resources.color = Faction_Player.FactionColor;
            }
        }

        // Set GUI element to match faction unit count
        private void SetUnits()
        {
            if (Text_PlayerInfo_Units != null && Faction_Player.Units != null)
                Text_PlayerInfo_Units.text = Faction_Player.Units.Count.ToString();
        }

        // Set GUI element to match faction resource count
        private void SetResources()
        {
            if (Text_PlayerInfo_Resources != null)
                Text_PlayerInfo_Resources.text = Faction_Player.Resources.ToString();
        }
    }
}