using System.Collections.Generic;
using UnityEngine;

namespace BBehaviour.Runtime {
    public class NavigationPoint : MonoBehaviour{
        [Header("Destination transform for the camera rig / player.")]
        public Transform destination;
        [Tooltip("Minimal distance before we snap to final position.")]
        // public List<Transform> relatedDestinations = new();
        public Dictionary<Directions, Transform> directions = new();
    }

    public enum Directions { Forward, Backward, Left, Right }
}
