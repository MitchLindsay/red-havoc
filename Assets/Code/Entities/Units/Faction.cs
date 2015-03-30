using Assets.Code.Entities.Tiles;
using Assets.Code.Entities.Units.Commands;
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
        public Color ActiveColor = Color.white;
        public Color InactiveColor = Color.gray;
        public int TurnPriority = 0;
        public List<Unit> Units { get; private set; }
        public List<UnitCommand> Commands { get; private set; }

        [HideInInspector]
        public int Resources = 0;

        void Awake()
        {
            RemoveAllUnits();
            RemoveAllCommands();
            AddChildUnits();
            SetCommands();
            DeactivateAllUnits();
        }

        public void AddUnit(Unit unit)
        {
            if (!Units.Contains(unit))
            {
                SetFaction(unit, this);
                SetColor(unit, ActiveColor);
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
                SetFaction(unit, null);
                SetColor(unit, Color.white);
                Units.Remove(unit);
            }
        }

        public void RemoveAllUnits()
        {
            Units = new List<Unit>();
        }

        public void SetFaction(Unit unit, Faction faction)
        {
            unit.SetFaction(faction);
        }

        public void SetColor(Unit unit, Color color)
        {
            if (unit != null)
                unit.GetComponent<SpriteRenderer>().color = color;
        }

        public void SetCommands()
        {
            CaptureCommand captureCommand = new CaptureCommand();
            AttackCommand attackCommand = new AttackCommand();
            MoveCommand moveCommand = new MoveCommand();
            WaitCommand waitCommand = new WaitCommand();

            AddCommand(captureCommand);
            AddCommand(attackCommand);
            AddCommand(moveCommand);
            AddCommand(waitCommand);
        }

        public void AddCommand(UnitCommand command)
        {
            if (command != null && !Commands.Contains(command))
                Commands.Add(command);
        }

        public void RemoveCommand(UnitCommand command)
        {
            if (Commands.Contains(command))
                Commands.Remove(command);
        }

        public void RemoveAllCommands()
        {
            Commands = new List<UnitCommand>();
        }

        public UnitCommand GetCommand(UnitCommandType commandType)
        {
            foreach (UnitCommand command in Commands)
            {
                if (command.CommandType == commandType)
                    return command;
            }

            return null;
        }

        public void ExecuteCaptureCommand(Unit unit, Tile tile)
        {
            UnitCommand waitCommand = GetCommand(UnitCommandType.Capture);
            waitCommand.Execute(unit.gameObject, tile.gameObject);
        }

        public void ExecuteAttackCommand(Unit attacker, Unit defender)
        {
            UnitCommand waitCommand = GetCommand(UnitCommandType.Attack);
            waitCommand.Execute(attacker.gameObject, defender.gameObject);
        }

        public void ExecuteMoveCommand(Unit unit, Tile tile)
        {
            UnitCommand waitCommand = GetCommand(UnitCommandType.Move);
            waitCommand.Execute(unit.gameObject, tile.gameObject);
        }

        public void ExecuteWaitCommand(Unit unit)
        {
            UnitCommand waitCommand = GetCommand(UnitCommandType.Wait);
            waitCommand.Execute(unit.gameObject);
        }

        public void ActivateUnit(Unit unit)
        {
            if (Units.Contains(unit))
            {
                SetColor(unit, ActiveColor);
                unit.IsActive = true;
            }
        }

        public void DeactivateUnit(Unit unit)
        {
            if (Units.Contains(unit))
            {
                SetColor(unit, InactiveColor);
                unit.IsActive = false;
            }
        }

        public void ActivateAllUnits()
        {
            foreach (Unit unit in Units)
                ActivateUnit(unit);
        }

        public void DeactivateAllUnits()
        {
            foreach (Unit unit in Units)
                DeactivateUnit(unit);
        }
    }
}