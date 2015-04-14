using Assets.Code.Generic;

namespace Assets.Code.Controllers
{
    public class InputHandler : Singleton<InputHandler>
    {
        public bool InputEnabled { get; private set; }
        public Actors.Cursor MouseCursor;
        public CameraHandler CameraHandler;

        void Awake()
        {
            if (CameraHandler == null)
                CameraHandler = CameraHandler.Instance;
        }

        public void EnableInput()
        {
            InputEnabled = true;
            UnityEngine.Cursor.visible = true;

            if (MouseCursor != null)
                MouseCursor.CursorEnabled = true;

            if (CameraHandler != null)
                CameraHandler.DragEnabled = true;
        }

        public void DisableInput()
        {
            InputEnabled = false;
            UnityEngine.Cursor.visible = false;

            if (MouseCursor != null)
                MouseCursor.CursorEnabled = false;

            if (CameraHandler != null)
                CameraHandler.DragEnabled = false;
        }
    }
}