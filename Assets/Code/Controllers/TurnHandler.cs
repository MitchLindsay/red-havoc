using Assets.Code.Actors;
using Assets.Code.Generic;
using System;
using UnityEngine;

namespace Assets.Code.Controllers
{
    public class TurnHandler : Singleton<TurnHandler>
    {
        public Faction[] Factions { get; private set; }
        public int TurnCount { get; set; }
        public int CurrentFactionIndex { get; set; }

        void Awake()
        {
            TurnCount = 0;
            CurrentFactionIndex = 0;

            AddExistingFactions();
            DeactivateAllFactions();
        }

        private void AddExistingFactions()
        {
            Factions = GameObject.FindObjectsOfType<Faction>();
            SortFactionsByTurnPriority();
        }

        private void SortFactionsByTurnPriority()
        {
            Array.Sort(Factions, delegate(Faction faction1, Faction faction2)
            {
                return faction1.TurnPriority.CompareTo(faction2.TurnPriority);
            });
        }

        public Faction ActiveFaction
        {
            get
            {
                foreach(Faction faction in Factions)
                    if (faction.IsActive)
                        return faction;

                return null;
            }
        }

        public void ActivateFaction(Faction faction)
        {
            faction.IsActive = true;
            faction.ActivateAllUnits();
        }

        public void ActivateAllFactions()
        {
            foreach (Faction faction in Factions)
                ActivateFaction(faction);
        }

        public void DeactivateFaction(Faction faction)
        {
            faction.IsActive = false;
            faction.DeactivateAllUnits();
        }

        public void DeactivateAllFactions()
        {
            foreach (Faction faction in Factions)
                DeactivateFaction(faction);
        }

        public void ChangeTurns()
        {
            TurnCount++;

            ActivateAllFactions();
            DeactivateAllFactions();

            if (CurrentFactionIndex >= Factions.GetLength(0))
                CurrentFactionIndex = 0;

            if (Factions.GetLength(0) > 0)
            {
                ActivateFaction(Factions[CurrentFactionIndex]);
                CurrentFactionIndex++;
            }
        }
    }
}