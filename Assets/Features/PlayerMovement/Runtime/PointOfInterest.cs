using UnityEngine;
using CameraManager.Runtime;

namespace PlayerMovement.Runtime
{
    using BBehaviour.Runtime;

    public class PointOfInterest : BBehaviour
    {
        public NavigationPoint navigationPointToTP;

        private void OnMuseDown()
        {
            PlayerMovementManager.instance.TeleportToPosition(navigationPointToTP.navigationData[0]);
        }
    }
}