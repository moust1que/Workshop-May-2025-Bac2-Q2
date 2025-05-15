using UnityEngine;

namespace Shuriken.Runtime {
    public class RotateCylinder : MonoBehaviour {
        public float step = 90f;
        public float targetAngle = 0f;

        public ShurikenEnigma manager;

        void OnMouseDown() {
            if (manager.allCorrect) return;
            transform.Rotate(0f, 0f, step);
            manager.CheckPuzzle();
        }

        public bool IsCorrect(float tolerance = 1f) {
            float current = transform.localEulerAngles.z;
            return Mathf.Abs(Mathf.DeltaAngle(current, targetAngle)) < tolerance;
        }
    }
}
