using Assets.Code.Controllers.Abstract;

namespace Assets.Code.Controllers.States
{
    public class ConfirmUnitCommandState : State
    {
        public override void OnInitialized()
        {
            this.StateID = StateID.ConfirmingUnitCommand;
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
