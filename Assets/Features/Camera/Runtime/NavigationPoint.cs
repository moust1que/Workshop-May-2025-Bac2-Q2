using UnityEngine;

namespace BBehaviour.Runtime {
    public class NavigationPoint : MonoBehaviour{
        [Header("Destination transform for the camera rig / player.")]
        public Transform destination;
        [Tooltip("Minimal distance before we snap to final position.")]
        public float stoppingDistance = 0.05f;
    }
}
