using UnityEngine;
using System.Collections;


namespace CameraManager.Runtime {
    using BBehaviour.Runtime;
    using UnityEngine.UI;

    public class CameraController : BBehaviour {

        public static CameraController Instance { get; private set; }

        public Texture2D cursorTexture;
        public Texture2D overCursorTexture;
        public CursorMode cursorMode = CursorMode.Auto;
        public Vector2 hotSpot = Vector2.zero;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }

        public void MoveTo(Transform destination) {
            transform.SetPositionAndRotation(destination.position, destination.rotation);
        }

    }
}
