using Assets.Code.Controllers;
using Assets.Code.Events;
using Assets.Code.Events.Events;
using Assets.Code.UI.Static;
using UnityEngine;

namespace Assets.Code.States.States
{
    public class StartBattleState : State
    {
        public StartBattleState(StateID currentStateID) : base(currentStateID) { }

        public override void SetTransitions()
        {
            StateTransition changingTurns = new StateTransition(TransitionID.Next, StateID.ChangeTurns);
            AddTransition(changingTurns);
        }

        public override void SetTransitionEvents()
        {
            StateTransition changingTurns = GetTransitionByID(TransitionID.Next);
            if (changingTurns != null)
            {
                // 1. Disable Input
                DisableInputEvent disableInputEvent = new DisableInputEvent(EventID.DisableInput, this, 
                    new EventArgs<InputHandler>(stateMachine.InputHandler));
                changingTurns.AddEvent(disableInputEvent, CoroutineID.Execute);

                // 2. Show "Start Battle" Splash Screen
                SplashScreen splashScreen = GameObject.Find("Start Battle").GetComponent<SplashScreen>();
                ShowSplashScreenEvent showSplashScreenEvent = new ShowSplashScreenEvent
                    (EventID.ShowSplashScreen, this, new EventArgs<SplashScreen, float>(splashScreen, 3.0f));
                changingTurns.AddEvent(showSplashScreenEvent, CoroutineID.Execute);
            }
        }

        public override void OnEntry()
        {
            base.OnEntry();
            RunEventsByTransitionID(TransitionID.Next);
        }

        public override void Update(float deltaTime) { }
    }
}