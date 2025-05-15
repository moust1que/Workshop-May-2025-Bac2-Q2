using NUnit.Framework;
using UnityEngine;

namespace Shuriken.Runtime {
    public class WindowOpening : MonoBehaviour {
       public GameObject window;
       public Transform openWindow;

       Vector3 startPos;

       float speed = 1f;

       public bool IsOpen;

        void Awake() {
            IsOpen = false;
            startPos = window.transform.position;
        }

        void OnMouseDown() {
            IsOpen = !IsOpen;
        }

        void Update() {
            Vector3 target = IsOpen ? openWindow.position : startPos;

            if (window.transform.position != target) {
                window.transform.position = Vector3.MoveTowards(
                    window.transform.position,
                    target, 
                    speed * Time.deltaTime 
                );
            }
        }
    }
}

