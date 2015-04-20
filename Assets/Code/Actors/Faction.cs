using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Actors
{
    public enum FactionPlayer
    {
        Null,
        Player,
        Computer
    }

    public class Faction : MonoBehaviour
    {
        public FactionPlayer FactionPlayer = FactionPlayer.Null;
        public string Name = "Faction";
        public int TurnPriority = 0;
        public int Resources { get; private set; }
        public bool IsActive { get; set; }
        public Color UnitColor = Color.gray;
        public List<Unit> Units { get; private set; }

        void Awake()
        {
            Resources = 0;

            RemoveAllUnits();
            AddExistingUnits();
            ApplyColorToAllUnits(UnitColor);
            DeactivateAllUnits();
        }

        public void AddUnit(Unit unit)
        {
            if (unit != null && !Units.Contains(unit))
            {
                if (Units.Count <= 0)
                    Units = new List<Unit>();

                ApplyColorToUnit(UnitColor, unit);
                Units.Add(unit);
            }
        }

        public void AddExistingUnits()
        {
            Unit[] units = gameObject.GetComponentsInChildren<Unit>();

            foreach (Unit unit in units)
                AddUnit(unit);
        }

        public void RemoveUnit(Unit unit)
        {
            if (unit != null && Units.Contains(unit))
            {
                ApplyColorToUnit(Color.gray, unit);
                Units.Remove(unit);
            }
        }

        public void RemoveAllUnits()
        {
            if (Units != null && Units.Count > 0)
            {
                foreach (Unit unit in Units)
                {
                    ApplyColorToUnit(Color.white, unit);
                    Units.Remove(unit);
                }
            }

            Units = new List<Unit>();
        }

        public void ApplyColorToUnit(Color color, Unit unit)
        {
            if (unit != null && Units.Contains(unit))
                unit.GetComponent<SpriteRenderer>().color = color;
        }

        public void ApplyColorToAllUnits(Color color)
        {
            foreach (Unit unit in Units)
                ApplyColorToUnit(color, unit);
        }

        public void ActivateUnit(Unit unit)
        {
            unit.IsActive = true;
            ApplyColorToUnit(UnitColor, unit);
        }

        public void ActivateAllUnits()
        {
            IsActive = true;

            foreach (Unit unit in Units)
                ActivateUnit(unit);
        }

        public void DeactivateUnit(Unit unit)
        {
            unit.IsActive = false;

            if (IsActive)
                ApplyColorToUnit(Color.gray, unit);
        }

        public void DeactivateAllUnits()
        {
            IsActive = false;

            if (Units != null)
            {
                foreach (Unit unit in Units)
                    DeactivateUnit(unit);
            }
        }
    }
}