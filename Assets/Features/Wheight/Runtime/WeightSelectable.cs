using UnityEngine;

namespace Wheight.Runtime
{
    public class WeightSelectable : MonoBehaviour
    {
        public Transform pickupSlot;

        [Header("Valeur (kg)")]
        public int weightValue = 5;

        public float height;

        private bool isCollected = false;
        private Renderer rend;

         void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(1))          
            {
                var mgr = WeightManager.Instance;
                if (mgr.IsOnPan(this))
                {
                    mgr.RemoveWeight(this);
                    Teleport(pickupSlot);
                    mgr.SelectWeight(null);         
                }
                return;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                var mgr = WeightManager.Instance;

                if (!isCollected)
                {
                    isCollected = true;
                    Teleport(pickupSlot);
                    mgr.SelectWeight(this);
                    return;
                }

                if (mgr.IsOnPan(this))
                    mgr.RemoveWeight(this);

                Teleport(pickupSlot);
                mgr.SelectWeight(this);
            }
        }
        
        public void Teleport(Transform target)
        {
            // transform.position = target.position;
            transform.rotation = pickupSlot.rotation;
            transform.position = new Vector3(target.position.x, target.position.y + height / 2, target.position.z);
            transform.rotation = target.rotation;
        }
    }
}
