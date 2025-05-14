using UnityEngine;

namespace CameraManager.Runtime{
    public class MouseHover : MonoBehaviour {
        public Texture2D cursorTexture;
        public Texture2D overCursorTexture;
        public CursorMode cursorMode = CursorMode.Auto;
        public Vector2 hotSpot = Vector2.zero;

        public void MouseHoverElement(){
            Cursor.SetCursor(overCursorTexture, hotSpot, cursorMode);
        }

        public void MouseExitElement(){
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
    }
    
}
