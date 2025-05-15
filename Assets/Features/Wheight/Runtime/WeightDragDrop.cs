using UnityEngine;

namespace Wheight.Runtime
{
    public class WeightDragDrop : MonoBehaviour
    {
        public int weightValue = 5;

        private Vector3 initialPos;
        private Quaternion initialRot;

        private Camera cam;
        private bool isDragging = false;
        private Vector3 offset;

        private void Start()
        {
            cam = Camera.main;
            initialPos = transform.position;
            initialRot = transform.rotation;
        }

        private void OnMouseDown()
        {
            Vector3 worldMouse = cam.ScreenToWorldPoint(Input.mousePosition);
            offset = transform.position - new Vector3(worldMouse.x, worldMouse.y, transform.position.z);
            isDragging = true;
        }

        private void OnMouseDrag()
        {
            if (!isDragging) return;
            Vector3 worldMouse = cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(worldMouse.x, worldMouse.y, transform.position.z) + offset;
        }

        private void OnMouseUp()
        {
            isDragging = false;
            WeightManager.Instance.TryPlaceWeight(this);
        }

        public void ResetToInitialPosition()
        {
            transform.position = initialPos;
            transform.rotation = initialRot;
        }
    }
}
