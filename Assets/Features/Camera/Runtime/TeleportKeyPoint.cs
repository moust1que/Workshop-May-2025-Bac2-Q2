using UnityEngine;
using Events.Runtime;

namespace CameraManager.Runtime {
    using BBehaviour.Runtime;

    public class TeleportKeyPoint : BBehaviour
    {
        [SerializeField] private Transform destinationSocket;
        [SerializeField] private string goalDestinationId;
        [SerializeField] private bool isPlayerMovement = false;

        private void OnMouseDown()
        {
            Verbose("OnMouseDown");
            if (CameraController.Instance != null)
            {
                CameraController.Instance.MoveTo(destinationSocket != null ? destinationSocket : transform);
                if (goalDestinationId != "")
                    GameEvents.OnTeleport?.Invoke(goalDestinationId);

                if (isPlayerMovement)
                {
                    GameEvents.OnPlayerMoved?.Invoke(destinationSocket);
                }
            }
        }

        // #if UNITY_EDITOR
        //     private void OnDrawGizmos() {
        //         Gizmos.color = Color.magenta;
        //         var t = destinationSocket != null ? destinationSocket : transform;
        //         Gizmos.DrawFrustum(t.position, 60, .3f, .01f, 1);
        //     }
        // #endif
    }
}
