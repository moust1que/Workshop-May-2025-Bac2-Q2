using System.Collections.Generic;
using UnityEngine;

namespace CameraManager.Runtime {
    public class NavigationPoint : MonoBehaviour{
        [System.Serializable]
        public class NavEntry {
            public Directions key;
            public Transform value;
        }

        public List<NavEntry> navigationData = new();
    }

    public enum Directions { Forward, Backward, Left, Right }
}
