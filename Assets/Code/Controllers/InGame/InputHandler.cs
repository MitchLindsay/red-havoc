using Assets.Code.Controllers.StateMachine.States;
using Assets.Code.Entities.Units;
using UnityEngine;

namespace Assets.Code.Controllers.InGame
{
    public class InputHandler : MonoBehaviour
    {
        public delegate void KeyPressHandler();
        public static event KeyPressHandler OnBackButtonPress;

        private bool inputEnabled;
        public KeyCode BackButton = KeyCode.Escape;

        void OnEnable()
        {
            CameraHandler.OnPanStart += DisableInput;
            StartBattleState.OnStateEntry += DisableInput;
            ChangeTurnsState.OnStateEntry += DisableInput;
            SelectUnitState.OnStateEntry += DisableInput;
            MoveUnitState.OnStateEntry += EnableInput;
            Unit.OnMoveStart += DisableInput;
            MoveUnitState.OnUnitDeselect += DisableInput;
            SelectUnitCommandState.OnStateEntry += EnableInput;
            SelectUnitCommandState.OnMoveCancel += DisableInput;
        }

        void OnDestroy()
        {
            CameraHandler.OnPanStart -= DisableInput;
            StartBattleState.OnStateEntry -= DisableInput;
            ChangeTurnsState.OnStateEntry -= DisableInput;
            SelectUnitState.OnStateEntry -= DisableInput;
            MoveUnitState.OnStateEntry -= EnableInput;
            Unit.OnMoveStart -= DisableInput;
            MoveUnitState.OnUnitDeselect -= DisableInput;
            SelectUnitCommandState.OnStateEntry += EnableInput;
            SelectUnitCommandState.OnMoveCancel += DisableInput;
        }

        void Update()
        {
            if (inputEnabled)
            {
                if (Input.GetKeyDown(BackButton) && OnBackButtonPress != null)
                    OnBackButtonPress();
            }
        }

        private void EnableInput()
        {
            inputEnabled = true;
        }

        private void DisableInput()
        {
            inputEnabled = false;
        }
    }
}