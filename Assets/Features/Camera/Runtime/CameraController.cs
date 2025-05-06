using UnityEngine;
using System.Collections;

namespace BBehaviour.Runtime {
    public class CameraController : MonoBehaviour{
        [Header("Movement settings")]
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float rotateSpeed = 360f;
        [SerializeField] private bool  autoFaceDestination = true; 

        [Header("Manual yaw (smooth)")]
        [SerializeField] private float yawStep = 90f;
        [SerializeField] private float yawDuration = 0.4f;

        [Header("Recenter settings")]
        [SerializeField] private KeyCode recenterKey = KeyCode.Space;
        [SerializeField] private float searchRadius = 35f;

        private Transform currentRoomCenter;

        private Camera cam;
        private Coroutine moveRoutine;
        private Coroutine yawRoutine;

        private void Awake(){
            cam = GetComponentInChildren<Camera>();
            if (!cam) Debug.LogWarning("CameraController: aucune Camera trouvée");
        }

        private void Update(){
            HandleYawInput();           
            HandleClickNavigation(); 
            RecenterToRoom();   
        }

        private void HandleYawInput(){
            if (Input.GetKeyDown(KeyCode.LeftArrow))  StartYaw(-yawStep);
            if (Input.GetKeyDown(KeyCode.RightArrow)) StartYaw(+yawStep);
        }

        private void StartYaw(float deltaYaw){
            if (yawRoutine != null) StopCoroutine(yawRoutine);
            yawRoutine = StartCoroutine(SmoothYaw(deltaYaw));
        }

        private IEnumerator SmoothYaw(float deltaYaw){
            Quaternion from = transform.rotation;
            Quaternion to = from * Quaternion.Euler(0f, deltaYaw, 0f);

            float t = 0f;
            while (t < 1f){
                t += Time.deltaTime / yawDuration;
                transform.rotation = Quaternion.Slerp(from, to, Mathf.SmoothStep(0f, 1f, t));
                yield return null;
            }
            transform.rotation = to;
            yawRoutine = null;
        }

        private void HandleClickNavigation(){
            if (Input.GetMouseButtonDown(0)){
                if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)){
                    var nav = hit.collider.GetComponent<NavigationPoint>();
                    if (nav && nav.destination){
                        if (moveRoutine != null) StopCoroutine(moveRoutine);
                        moveRoutine = StartCoroutine(MoveTo(nav.destination, nav.stoppingDistance));
                    }
                }
            }
        }

        private IEnumerator MoveTo(Transform dest, float stoppingDistance){
            while (Vector3.Distance(transform.position, dest.position) > stoppingDistance){
                transform.position = Vector3.MoveTowards(transform.position,
                                                        dest.position,
                                                        moveSpeed * Time.deltaTime);

                if (autoFaceDestination && yawRoutine == null){
                    transform.rotation = Quaternion.RotateTowards(transform.rotation,
                                                                dest.rotation,
                                                                rotateSpeed * Time.deltaTime);
                }
                yield return null;
            }
            moveRoutine = null;
        }

        private void HandleRecenterInput(){
            if (Input.GetKeyDown(recenterKey)){
                Transform target = GetCurrentRoomCenter();
                if (target && moveRoutine == null)          // on évite d’interrompre un déplacement manuel
                    moveRoutine = StartCoroutine(MoveTo(target, 0.1f));
            }
        }

        private Transform GetCurrentRoomCenter(){
            // 1 Si  mémorisé et qu’il existe encore on le renvoie
            if (currentRoomCenter) return currentRoomCenter;

            // 2  Sinon on cherche le plus proche objet taggé "RoomCenter"
            Collider[] hits = Physics.OverlapSphere(transform.position, searchRadius);
            float bestDist = float.MaxValue;
            Transform best = null;
            foreach (var c in hits){
                if (c.CompareTag("RoomCenter")){
                    float d = (c.transform.position - transform.position).sqrMagnitude;
                    if (d < bestDist) { bestDist = d; best = c.transform; }
                }
            }
            return best;
        }

        private void OnTriggerEnter(Collider other){
            if (other.CompareTag("RoomCenter"))
                currentRoomCenter = other.transform;
        }

        private void OnTriggerExit(Collider other){
            if (other.transform == currentRoomCenter)
                currentRoomCenter = null;   // on sort de la pièce on oublie son centre
        }
        private void RecenterToRoom() => HandleRecenterInput();
    }
}