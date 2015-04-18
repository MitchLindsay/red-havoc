﻿using Assets.Code.Generic;
using UnityEngine;

namespace Assets.Code.Controllers
{
    public class InputHandler : Singleton<InputHandler>
    {
        public delegate void BackButtonHandler();
        public static event BackButtonHandler OnBackButtonPress;

        public bool InputEnabled { get; private set; }
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
            if (InputEnabled)
            {
                if (Input.GetKey(BackButton) && OnBackButtonPress != null)
                    OnBackButtonPress();
            }
        }

        public void EnableInput()
        {
            InputEnabled = true;

            if (MouseCursor != null)
                MouseCursor.CursorEnabled = true;

            if (CameraHandler != null)
                CameraHandler.DragEnabled = true;
        }

        public void DisableInput()
        {
            InputEnabled = false;

            if (MouseCursor != null)
                MouseCursor.CursorEnabled = false;

            if (CameraHandler != null)
                CameraHandler.DragEnabled = false;
        }
    }
}