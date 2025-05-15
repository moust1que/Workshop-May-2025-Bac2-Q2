using UnityEngine;

namespace Wheight.Runtime
{
    public class WeightSelectable : MonoBehaviour
    {
        [Header("Pickup slot")]
        public Transform pickupSlot;

        [Header("Valeur (kg)")]
        public int weightValue = 5;

        /* ----- état interne ----- */
        private bool isCollected = false;
        private Renderer rend;
        private Color   baseColor;

        void Start()
        {
            rend      = GetComponent<Renderer>();
            baseColor = rend.material.color;
        }

        void OnMouseDown()
        {
            // 1) premier clic = ramassage
            if (!isCollected)
            {
                isCollected = true;
                Teleport(pickupSlot);
                WeightManager.Instance.SelectWeight(this);
                return;
            }

            // 2) si déjà sur un plateau → on le retire et le sélectionne
            WeightManager.Instance.RemoveWeight(this);
            Teleport(pickupSlot);
            WeightManager.Instance.SelectWeight(this);
        }

        public void Teleport(Transform target)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }

        /* ----- feedback visuel ----- */
        public void SetHighlight(bool on)
        {
            rend.material.color = on ? Color.yellow : baseColor;
        }
    }
}
