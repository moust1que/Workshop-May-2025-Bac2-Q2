using NUnit.Framework;
using UnityEngine;

namespace Shuriken.Runtime {
    using Attribute.Runtime;
    public class WindowOpening : MonoBehaviour
    {
        public GameObject window;
        public Transform openWindow;

        private DelayManager delayManager;
        public float delayTime = 3f;
        public float baseDelayTime = 3f;

        Vector3 startPos;

        float speed = 1f;

        public bool IsOpen;
        public bool isAutoClose;

        void Awake() {
            IsOpen = false;
            startPos = window.transform.position;

            delayManager = gameObject.AddComponent<DelayManager>();
        }

        void OnMouseDown() {
            IsOpen = !IsOpen;
        }

        void Update() {
            Vector3 target = IsOpen ? openWindow.position : startPos;

            if (window.transform.position != target)
            {
                window.transform.position = Vector3.MoveTowards(
                    window.transform.position,
                    target,
                    speed * Time.deltaTime
                );
            }
            AutoClose();
        }

        void AutoClose() {
            if(isAutoClose == false) return;
            delayManager.Delay(delayTime, () => IsOpen = false);
            delayTime = baseDelayTime;
        }
    }
}

