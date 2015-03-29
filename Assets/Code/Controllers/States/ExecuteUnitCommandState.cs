using Assets.Code.Controllers.Abstract;

namespace Assets.Code.Controllers.States
{
    public class ExecuteUnitCommandState : State
    {
        public override void OnInitialized()
        {
            this.StateID = StateID.ExecutingUnitCommand;
        }

        public override void OnEntry()
        {
            base.OnEntry();
        }

        public override void Reason()
        {

        }

        public override void Update(float deltaTime)
        {

        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
