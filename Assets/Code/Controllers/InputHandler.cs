using Assets.Code.Generic;

namespace Assets.Code.Controllers
{
    public class InputHandler : Singleton<InputHandler>
    {
        public bool InputEnabled { get; private set; }
        public Actors.Cursor MouseCursor;

        public void EnableInput()
        {
            InputEnabled = true;
            UnityEngine.Cursor.visible = true;

            if (MouseCursor != null)
                MouseCursor.CursorEnabled = true;
        }

        public void DisableInput()
        {
            InputEnabled = false;
            UnityEngine.Cursor.visible = false;

            if (MouseCursor != null)
                MouseCursor.CursorEnabled = false;
        }
    }
}