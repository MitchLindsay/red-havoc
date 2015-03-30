using UnityEngine;

namespace Assets.Code.Entities.Units
{
    public enum UnitCommandType
    {
        Null = 0,
        Capture = 1,
        Attack = 2,
        Move = 3,
        Wait = 4,
    }

    public abstract class UnitCommand
    {
        public UnitCommandType CommandType { get; protected set; }
        protected Faction faction;

        internal void SetFaction(Faction faction)
        {
            this.faction = faction;
            Initialize();
        }

        public virtual void Initialize() { }
        public virtual void Execute() { }
        public virtual void Execute(GameObject referencedObj) { }
        public virtual void Execute(GameObject referencedObj1, GameObject referencedObj2) { }
        public virtual void OnCompleted() { }
    }
}