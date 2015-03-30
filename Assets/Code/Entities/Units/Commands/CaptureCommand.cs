using Assets.Code.Entities.Tiles;
using UnityEngine;

namespace Assets.Code.Entities.Units.Commands
{
    public class CaptureCommand : UnitCommand
    {
        public delegate void ExecutionHandler();
        public static event ExecutionHandler OnExecute;
        public static event ExecutionHandler OnComplete;

        public override void Initialize()
        {
            this.CommandType = UnitCommandType.Capture;
        }

        public override void Execute(GameObject unitObj, GameObject tileObj)
        {
            if (OnExecute != null)
                OnExecute();

            if (unitObj != null && tileObj != null)
            {
                Unit unit = unitObj.GetComponent<Unit>();
                Tile tile = tileObj.GetComponent<Tile>();

                if (unit != null && tile != null)
                    Debug.Log(unit.EntityName + " captured " + tile.EntityName);
            }
        }

        public override void OnCompleted()
        {
            if (OnComplete != null)
                OnComplete();
        }
    }
}