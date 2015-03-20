using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Units.Entities
{
    public enum FactionController
    {
        None,
        Player,
        AI
    }

    // Faction.cs - Represents an individual player, contains entities controlled by the faction
    public class Faction : MonoBehaviour
    {
        // Faction name, edited through the unity interface
        public string FactionName = "Faction";

        // Faction controller, edited through the unity interface
        public FactionController FactionController = FactionController.None;

        // Color of the faction, edited through the unity interface
        public Color FactionColor = Color.white;

        // List of controlled units
        public List<Unit> Units { get; private set; }

        // Mineral counter
        [HideInInspector]
        public int MineralCount = 0;

        // GUI elements, edited through Unity interface
        public Text UnitCountGUIText;
        public Text MineralCountGUIText;
        public Image UnitCountGUIImage;
        public Image MineralCountGUIImage;

        void Start()
        {
            // Remove all units in the list
            RemoveAllUnits();
            // Add all the child units
            AddChildUnits();
        }

        // Adds an unit to the list of controlled units
        public void AddUnit(Unit unit)
        {
            // Only add the unit if it is not already controlled
            if (!Units.Contains(unit))
            {
                SetColor(unit, FactionColor);
                Units.Add(unit);
            }
        }

        // Adds all child units to the list of controlled units
        public void AddChildUnits()
        {
            // Find all controlled units
            Unit[] units = gameObject.GetComponentsInChildren<Unit>();

            // Add controlled units
            foreach (Unit unit in units)
                AddUnit(unit);
        }

        // Removes a unit from the list of controlled units
        public void RemoveUnit(Unit unit)
        {
            // Only remove the unit if it is currently controlled
            if (Units.Contains(unit))
            {
                SetColor(unit, Color.white);
                Units.Remove(unit);
            }
        }

        // Removes all units
        public void RemoveAllUnits()
        {
            Units = new List<Unit>();
        }

        // Changes a unit's color
        public void SetColor(Unit unit, Color color)
        {
            // Check if unit exists
            if (unit != null)
                unit.GetComponent<SpriteRenderer>().color = color;
        }
    }
}