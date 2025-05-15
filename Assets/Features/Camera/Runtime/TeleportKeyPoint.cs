using UnityEngine;
using Events.Runtime;

namespace CameraManager.Runtime {
    public class TeleportKeyPoint : MonoBehaviour {
        [SerializeField] private Transform destinationSocket;
        [SerializeField] private string goalDestinationId;

        private void OnMouseDown()
        {
            if (CameraController.Instance != null)
            {
                CameraController.Instance.MoveTo(destinationSocket != null ? destinationSocket : transform);
                GameEvents.OnTeleport?.Invoke(goalDestinationId);
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
