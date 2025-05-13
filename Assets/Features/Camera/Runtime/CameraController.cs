using UnityEngine;
using System.Collections;

namespace CameraManager.Runtime {
    public class CameraController : MonoBehaviour {
        [Header("Movement settings")]
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float rotateSpeed = 360f;

        [Header("Manual yaw (smooth)")]
        [SerializeField] private float yawStep = 90f;
        [SerializeField] private float yawDuration = 0.4f;

        private Camera cam;

        

        private void Awake(){
            cam = GetComponentInChildren<Camera>();
        }

        private void Update(){

        }

        private void HandleClickNavigation(){
            if(Input.GetMouseButtonDown(0)){
                if(Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)){
                    var nav = hit.collider.GetComponent<NavigationPoint>();
                    if (nav/* && nav.destination*/){
                        // if (moveRoutine != null) StopCoroutine(moveRoutine);
                        // moveRoutine = StartCoroutine(MoveTo(nav.destination, nav.stoppingDistance));
                    }
                }
            }
        }

        // private IEnumerator MoveToS(Transform dest, float stoppingDistance){
        //     while (Vector3.Distance(transform.position, dest.position) > stoppingDistance){
        //         transform.position = Vector3.MoveTowards(transform.position,
        //                                                 dest.position,
        //                                                 moveSpeed * Time.deltaTime);

        //         // if (autoFaceDestination && yawRoutine == null){
        //         //     transform.rotation = Quaternion.RotateTowards(transform.rotation,
        //         //                                                 dest.rotation,
        //         //                                                 rotateSpeed * Time.deltaTime);
        //         // }
        //         yield return null;
        //     }
        //     // moveRoutine = null;
        // }

        void MoveTo(Transform dest){
            transform.position = dest.transform.position;
        }

    }
}