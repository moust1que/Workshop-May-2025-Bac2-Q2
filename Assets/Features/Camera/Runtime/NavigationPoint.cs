using System.Collections.Generic;
using UnityEngine;

namespace CameraManager.Runtime {
    using BBehaviour.Runtime;

    public class NavigationPoint : BBehaviour
    {
        [System.Serializable]
        public class NavEntry
        {
            public Directions key;
            public Transform value;
        }

        public List<NavEntry> navigationData = new();

        public List<GameObject> objectsToEnableOnArrival = new();

        public bool HasDirection(Directions direction)
        {
            return navigationData.Exists(x => x.key == direction);
        }
    }

    public enum Directions { Forward, Backward, Left, Right }
}