using UnityEngine;

namespace Wheight.Runtime
{
    public class WeightDragDrop : MonoBehaviour
    {
        // public Transform pickupSlot;

        // [Header("Valeur du poids")]
        // public int weightValue = 5;

        // // État interne
        // private Vector3 sceneStartPos;
        // private Quaternion sceneStartRot;
        // private bool isCollected = false;
        // private bool isDragging = false;
        // private Vector3 offset;
        // private Camera cam;

        // void Start()
        // {
        //     cam = Camera.main;
        //     sceneStartPos = transform.position;
        //     sceneStartRot = transform.rotation;
        // }

        // void OnMouseDown()
        // {
        //     if (!isCollected)
        //     {
        //         isCollected = true;
        //         transform.position = pickupSlot.position;
        //         transform.rotation = pickupSlot.rotation;
        //         sceneStartPos = pickupSlot.position;
        //         sceneStartRot = pickupSlot.rotation;
        //         return;
        //     }

        //     Vector3 worldMouse = cam.ScreenToWorldPoint(Input.mousePosition);
        //     offset = transform.position - new Vector3(worldMouse.x, worldMouse.y, transform.position.z);
        //     isDragging = true;
        // }

        // void OnMouseDrag()
        // {
        //     if (!isDragging) return;
        //     Vector3 worldMouse = cam.ScreenToWorldPoint(Input.mousePosition);
        //     transform.position = new Vector3(worldMouse.x, worldMouse.y, transform.position.z) + offset;
        // }

        // void OnMouseUp()
        // {
        //     if (!isDragging) return;
        //     isDragging = false;
        //     WeightManager.Instance.TryPlaceWeight(this);
        // }

        // public void ResetToInitialPosition()
        // {
        //     transform.position = sceneStartPos;
        //     transform.rotation = sceneStartRot;
        // }
    }
}

