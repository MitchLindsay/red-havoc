using Assets.Code.Generic;
using UnityEngine;

namespace Assets.Code.Controllers
{
    public class InputHandler : Singleton<InputHandler>
    {
        public delegate void InputDisableHandler();
        public delegate void BackButtonHandler();
        public static event InputDisableHandler OnCursorDisabled;
        public static event BackButtonHandler OnBackButtonPress;

        public bool CursorEnabled { get; private set; }
        public bool KeyboardEnabled { get; private set; }
        public Actors.Cursor MouseCursor;
        public CameraHandler CameraHandler;
        public KeyCode BackButton = KeyCode.Escape;

        void Awake()
        {
            if (CameraHandler == null)
                CameraHandler = CameraHandler.Instance;
        }

        void Update()
        {
            CheckForInput();
        }

        public void CheckForInput()
        {
            if (KeyboardEnabled)
            {
                if (Input.GetKey(BackButton) && OnBackButtonPress != null)
                    OnBackButtonPress();
            }
        }

        public void EnableInput()
        {
            EnableCursor();
            EnableKeyboard();
        }

        public void DisableInput()
        {
            DisableCursor();
            DisableKeyboard();
        }

        public void EnableKeyboard()
        {
            KeyboardEnabled = true;
        }

        public void DisableKeyboard()
        {
            KeyboardEnabled = false;
        }

        public void EnableCursor()
        {
            CursorEnabled = true;

            if (MouseCursor != null)
                MouseCursor.CursorEnabled = true;

            if (CameraHandler != null)
                CameraHandler.DragEnabled = true;
        }

        public void DisableCursor()
        {
            CursorEnabled = false;

            if (MouseCursor != null)
                MouseCursor.CursorEnabled = false;

            if (CameraHandler != null)
                CameraHandler.DragEnabled = false;

            if (OnCursorDisabled != null)
                OnCursorDisabled();
        }
    }
}