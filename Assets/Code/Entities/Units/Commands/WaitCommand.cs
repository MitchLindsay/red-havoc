using UnityEngine;

namespace Assets.Code.Entities.Units.Commands
{
    public class WaitCommand : UnitCommand
    {
        public delegate void ExecutionHandler();
        public static event ExecutionHandler OnExecute;
        public static event ExecutionHandler OnComplete;

        public override void Initialize()
        {
            this.CommandType = UnitCommandType.Wait;
        }

        public override void Execute(GameObject unitObj)
        {
            if (OnExecute != null)
                OnExecute();

            if (unitObj != null)
            {
                Unit unit = unitObj.GetComponent<Unit>();



                if (unit != null)
                    Debug.Log(unit.EntityName + " waited");
            }
        }

        public override void OnCompleted()
        {
            if (OnComplete != null)
                OnComplete();
        }
    }
}