using UnityEngine;

namespace GameManager.Runtime {
    public class GameManager : MonoBehaviour {
        [SerializeField] Transform spawnPoint;
        Camera mainCamera;

        void Start() {
            mainCamera = Camera.main;
            mainCamera.transform.position = spawnPoint.position;
            mainCamera.transform.rotation = spawnPoint.rotation;
        }
    }
}