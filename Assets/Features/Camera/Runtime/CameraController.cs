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
    }
}
