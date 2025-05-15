using UnityEngine;
using Goals.Runtime;

namespace BookShelf {
    using BBehaviour.Runtime;
    public class Book : MonoBehaviour {
        public GameObject bookObject;
        public BookShelfEnigma bookShelf;
        public Transform bookTargetPosition;
        public bool isBookSelected = false;

        public int orderIndex;
        public bool isDecoy = false;

        public float moveSpeed = 5f; 
        private bool isMoving = false;

        private Vector3 origPos;
        private Quaternion origRot;

        void Awake() {
            origPos = transform.position;
            origRot = transform.rotation;
        }

        void OnMouseDown() {
            if((bool)GoalsManager.instance.goals["ExitDoorClosed"].Progress.Value == true){
                if(bookShelf.allCorrect) return;
                if(isDecoy) return;
                isMoving = true;
                bookShelf.OnBookPlaced(this);
            }   
        }

        void Update() {
            if (!isMoving) return;

            transform.position = Vector3.MoveTowards(
                transform.position,
                bookTargetPosition.position,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, bookTargetPosition.position) < 0.01f) {
                transform.position = bookTargetPosition.position;
                isMoving = false;
            }
        }

        public void ResetPosition() {
            transform.position = origPos;
            transform.rotation = origRot;
            isMoving = false;
        }


    }
}
