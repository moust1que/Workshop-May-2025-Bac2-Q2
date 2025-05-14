using System.Collections.Generic;
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
            bool hoverNow = false;

            if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) {
                PointerEventData pointer = new PointerEventData(EventSystem.current) {
                    position = Input.mousePosition
                };
                List<RaycastResult> results = new();
                EventSystem.current.RaycastAll(pointer, results);
                
                foreach(RaycastResult res in results) {
                    if(res.gameObject.CompareTag("Hoverable")) {
                        hoverNow = true;
                        break;
                    }
                }

                SetCursor(hoverNow);
                return;
            }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit, 100f, hoverMask)) {
                if(hit.collider.CompareTag("Hoverable"))
                    hoverNow = true;
            }

            SetCursor(hoverNow);
        }

        void SetCursor(bool hover) {
            if(hover == isHovering) return;
            isHovering = hover;
            Cursor.SetCursor(hover ? hoverCursor : defaultCursor, hotspot, cursorMode);
        }
    }
}
