using UnityEngine;

namespace BookShelf {
    using System;
    using BBehaviour.Runtime;
    public class ShamisenShelf : BBehaviour
    {
        public GameObject leftDoor;
        public GameObject rightDoor;

        public float doorSpeed = 2f;


        public bool IsOpen = false;

        void Update() {
            if (IsOpen)
            {
                leftDoor.transform.localEulerAngles = Vector3.Lerp(leftDoor.transform.localEulerAngles, new Vector3(0, 120, 0), doorSpeed * Time.deltaTime);
                rightDoor.transform.localEulerAngles = Vector3.Lerp(rightDoor.transform.localEulerAngles, new Vector3(0, 240, 0), doorSpeed * Time.deltaTime);
            }
        }

    }
}
