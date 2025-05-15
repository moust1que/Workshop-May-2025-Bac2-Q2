using UnityEngine;

namespace Shuriken.Runtime {
    using Attribute.Runtime;
    public class WindowOpening : MonoBehaviour
    {
        public GameObject window;
        public Transform openWindow;

        public float delayTime = 3f;
        public float baseDelayTime = 3f;

        Vector3 startPos;

        public float openSpeed = 1f;
        public float closeSpeed = 1f;

        public bool IsOpen;
        public bool isAutoClose;

        void Awake() {
            IsOpen = false;
            startPos = window.transform.position;
        }

        void OnMouseDown()
        {
            ToggleWindow();
        }

         void ToggleWindow() {
            IsOpen = !IsOpen;

            if (IsOpen && isAutoClose) {
                DelayManager.instance.Delay(delayTime, () => IsOpen = false);
            }
        }

        void Update() {
            Vector3 target = IsOpen ? openWindow.position : startPos;
            float currentSpeed = IsOpen ? openSpeed : closeSpeed;

            if (window.transform.position != target)
            {
                window.transform.position = Vector3.MoveTowards(
                    window.transform.position,
                    target,
                    currentSpeed * Time.deltaTime
                );
            }
        }

        void AutoClose() {
            if(isAutoClose == false) return;
            DelayManager.instance.Delay(delayTime, () => IsOpen = false);
            delayTime = baseDelayTime;
        }
    }
}

