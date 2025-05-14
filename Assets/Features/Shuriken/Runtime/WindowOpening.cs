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

            // On ne bouge que si on n’est pas déjà à la cible
            if (window.transform.position != target) {
                window.transform.position = Vector3.MoveTowards(
                    window.transform.position,  // <-- current position
                    target,                      // <-- goal position
                    speed * Time.deltaTime       // <-- step this frame
                );
            }
        }
    }
}

