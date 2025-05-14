using UnityEngine;
using UnityEngine.EventSystems;

namespace CameraManager.Runtime {
    public class CursorManager : MonoBehaviour {
        
        [Header("Textures")]
        public Texture2D defaultCursor;
        public Texture2D hoverCursor;
        public Vector2   hotspot  = Vector2.zero;
        public CursorMode cursorMode = CursorMode.Auto;

        [Header("LayerMask des objets cliquables")]
        public LayerMask hoverMask = ~0;

        Camera cam;
        bool isHovering;

        void Awake() {
            cam = Camera.main;
            Cursor.SetCursor(defaultCursor, hotspot, cursorMode);
        }

        void Update() {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            bool hoverNow = false;

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, hoverMask)) {
                if (hit.collider.CompareTag("Hoverable"))
                    hoverNow = true;
            }

            if (!hoverNow && EventSystem.current != null) {
                var pointer = new PointerEventData(EventSystem.current){
                    position = Input.mousePosition
                };
                var results = new System.Collections.Generic.List<RaycastResult>();
                EventSystem.current.RaycastAll(pointer, results);

                foreach (var res in results) {
                    if (res.gameObject.CompareTag("Hoverable")) {
                        hoverNow = true;
                        break;
                    }
                }
            }

            if (hoverNow != isHovering) {
                isHovering = hoverNow;
                Cursor.SetCursor(isHovering ? hoverCursor : defaultCursor, hotspot, cursorMode);
            }
        }

        void SetCursor(bool hover) {
            if (hover == isHovering) return;
            isHovering = hover;
            Cursor.SetCursor(hover ? hoverCursor : defaultCursor, hotspot, cursorMode);
        }
    }
}
