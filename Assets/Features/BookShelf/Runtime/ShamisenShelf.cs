using UnityEngine;

namespace BookShelf {
    using System;
    using BBehaviour.Runtime;
    public class ShamisenShelf : MonoBehaviour
    {
        public GameObject leftDoor;
        public GameObject rightDoor;

        public float doorSpeed = 2f;


        public bool IsOpen = false;

        void Update() {
            if (IsOpen) {
                leftDoor.transform.localEulerAngles = Vector3.Lerp(leftDoor.transform.localEulerAngles, new Vector3(0, 95, 0), doorSpeed * Time.deltaTime);
                rightDoor.transform.localEulerAngles = Vector3.Lerp(rightDoor.transform.localEulerAngles, new Vector3(0, 95, 0), doorSpeed * Time.deltaTime);
                
            }
        }

    }
}
