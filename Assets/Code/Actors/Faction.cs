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
        public Color UnitColor = Color.white;
        public List<Unit> Units { get; private set; }

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

        public void RemoveUnit(Unit unit)
        {
            if (unit != null && Units.Contains(unit))
            {
                ApplyColorToUnit(Color.white, unit);
                Units.Remove(unit);
            }
        }

        public void RemoveUnits()
        {
            if (Units.Count > 0)
            {
                foreach (Unit unit in Units)
                {
                    ApplyColorToUnit(Color.white, unit);
                    Units.Remove(unit);
                }
            }
        }

        public void ApplyColorToUnit(Color color, Unit unit)
        {
            if (unit != null && Units.Contains(unit))
                unit.GetComponent<SpriteRenderer>().color = color;
        }
    }
}