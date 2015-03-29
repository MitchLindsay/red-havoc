using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Entities.Units
{
    public enum FactionController
    {
        None,
        Player,
        AI
    }

    public class Faction : MonoBehaviour
    {
        public string FactionName = "Faction";
        public FactionController FactionController = FactionController.None;
        public Color FactionColor = Color.white;
        public int TurnPriority = 0;
        public List<Unit> Units { get; private set; }

        [HideInInspector]
        public int Resources = 0;

        void Start()
        {
            RemoveAllUnits();
            AddChildUnits();
        }

        public void AddUnit(Unit unit)
        {
            if (!Units.Contains(unit))
            {
                SetColor(unit, FactionColor);
                Units.Add(unit);
            }
        }

        public void AddChildUnits()
        {
            Unit[] units = gameObject.GetComponentsInChildren<Unit>();

            foreach (Unit unit in units)
                AddUnit(unit);
        }

        public void RemoveUnit(Unit unit)
        {
            if (Units.Contains(unit))
            {
                SetColor(unit, Color.white);
                Units.Remove(unit);
            }
        }

        public void RemoveAllUnits()
        {
            Units = new List<Unit>();
        }

        public void SetColor(Unit unit, Color color)
        {
            if (unit != null)
                unit.GetComponent<SpriteRenderer>().color = color;
        }
    }
}