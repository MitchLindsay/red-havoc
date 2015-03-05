using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Units.Entities
{
    public enum UnitControllerType
    {
        None,
        Player,
        AI
    }

    public class UnitController : MonoBehaviour
    {
        // Unit controller name, edited through the unity interface
        public string ControllerName = "Controller";

        // Unit controller type, edited through the unity interface
        public UnitControllerType ControllerType = UnitControllerType.None;

        // Color of controlled units, edited through the unity interface
        public Color UnitColor = Color.white;

        // List of controlled units
        public List<Unit> Units { get; private set; }

        void Start()
        {
            // Remove all units in the list
            RemoveAllUnits();
            // Add all the child units
            AddChildUnits();
        }

        // Adds a unit to the list of controlled units
        public void AddUnit(Unit unit)
        {
            // Only add the unit if it is not already controlled
            if (!Units.Contains(unit))
            {
                SetUnitColor(unit, UnitColor);
                Units.Add(unit);
            }
        }
        
        // Adds all child units to the list of controlled units
        public void AddChildUnits()
        {
            Unit[] units = gameObject.GetComponentsInChildren<Unit>();

            foreach (Unit unit in units)
                AddUnit(unit);
        }

        // Removes a unit from the list of controlled units
        public void RemoveUnit(Unit unit)
        {
            // Only remove the unit if it is currently controlled
            if (Units.Contains(unit))
            {
                SetUnitColor(unit, Color.white);
                Units.Remove(unit);
            }
        }

        // Removes all units
        public void RemoveAllUnits()
        {
            Units = new List<Unit>();
        }

        // Changes a unit's color
        public void SetUnitColor(Unit unit, Color color)
        {
            // Check if unit exists
            if (unit != null)
                unit.GetComponent<SpriteRenderer>().color = color;
        }
    }
}