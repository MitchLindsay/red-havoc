using Assets.Code.Controllers.States;
using Assets.Code.Entities.Units;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Controllers
{
    public class TurnHandler : MonoBehaviour
    {
        public delegate void FactionChangeHandler(int turnCount, Faction activeFaction);
        public static event FactionChangeHandler OnFactionChangeComplete;

        private Faction activeFaction;
        private Faction[] factions;
        private int factionIndex;
        private int turnCount;

        void OnEnable()
        {
            StartBattleState.OnStateEntry += InitializeTurnCount;
            ChangeTurnState.OnStateEntry += NextActiveFaction;
        }

        void OnDestroy()
        {
            StartBattleState.OnStateExit -= InitializeTurnCount;
            ChangeTurnState.OnStateEntry -= NextActiveFaction;
        }

        void Awake()
        {
            InitializeFactions();
        }

        private void InitializeTurnCount()
        {
            factionIndex = 0;
            turnCount = 0;
        }

        private void InitializeFactions()
        {
            factions = GameObject.FindObjectsOfType<Faction>();
            SortFactionsByTurnPriority();
        }

        private void NextActiveFaction()
        {
            turnCount++;

            if (factionIndex >= factions.GetLength(0))
                factionIndex = 0;

            activeFaction = factions[factionIndex];
            factionIndex++;

            if (OnFactionChangeComplete != null)
                OnFactionChangeComplete(turnCount, activeFaction);
        }

        private void SortFactionsByTurnPriority()
        {
            Array.Sort(factions, delegate(Faction faction1, Faction faction2)
            {
                return faction1.TurnPriority.CompareTo(faction2.TurnPriority);
            });
        }
    }
}