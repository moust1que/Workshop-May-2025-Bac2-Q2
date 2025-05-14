using UnityEngine;
using System.Collections;


namespace CameraManager.Runtime {
    using BBehaviour.Runtime;
    public class CameraController : BBehaviour {

        public static CameraController Instance { get; private set; }

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void MoveTo(Transform destination) {
            transform.SetPositionAndRotation(destination.position, destination.rotation);
        }

    }
}