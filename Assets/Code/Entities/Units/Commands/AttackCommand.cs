using UnityEngine;

namespace Assets.Code.Entities.Units.Commands
{
    public class AttackCommand : UnitCommand
    {
        public delegate void ExecutionHandler();
        public static event ExecutionHandler OnExecute;
        public static event ExecutionHandler OnComplete;

        public override void Initialize()
        {
            this.CommandType = UnitCommandType.Attack;
        }

        public override void Execute(GameObject unitObj1, GameObject unitObj2)
        {
            if (OnExecute != null)
                OnExecute();

            if (unitObj1 != null && unitObj2 != null)
            {
                Unit unit1 = unitObj1.GetComponent<Unit>();
                Unit unit2 = unitObj2.GetComponent<Unit>();

                if (unit1 != null && unit2 != null)
                    Debug.Log(unit1.EntityName + " attacked " + unit2.EntityName);
            }
        }

        public override void OnCompleted()
        {
            if (OnComplete != null)
                OnComplete();
        }
    }
}