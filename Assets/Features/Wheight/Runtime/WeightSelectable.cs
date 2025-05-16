using UnityEngine;

namespace Wheight.Runtime
{
    public class WeightSelectable : MonoBehaviour
    {
        [Header("Pickup slot")]
        public Transform pickupSlot;

        [Header("Valeur (kg)")]
        public int weightValue = 5;

        public float height;

        /* ----- état interne ----- */
        private bool isCollected = false;
        private Renderer rend;
        // private Color   baseColor;

        void Start()
        {
            // rend      = GetComponent<Renderer>();
            // baseColor = rend.material.color;
        }

         void OnMouseOver()
        {
            /* ----- clic-droit = retrait du plateau ----- */
            if (Input.GetMouseButtonDown(1))           // bouton droit
            {
                // si sur un plateau, on le renvoie au pickupSlot sans le sélectionner
                var mgr = WeightManager.Instance;
                if (mgr.IsOnPan(this))
                {
                    mgr.RemoveWeight(this);
                    Teleport(pickupSlot);
                    mgr.SelectWeight(null);            // aucune sélection active
                }
                return;
            }

            /* ----- clic-gauche = logique normale ----- */
            if (Input.GetMouseButtonDown(0))
            {
                var mgr = WeightManager.Instance;

                // a) premières fois = ramassage
                if (!isCollected)
                {
                    isCollected = true;
                    Teleport(pickupSlot);
                    mgr.SelectWeight(this);
                    return;
                }

                // b) déjà collecté, mais peut être encore dans le pickup ou sur un plateau
                //    -> si sur plateau on le retire, sinon on (re)sélectionne juste
                if (mgr.IsOnPan(this))
                    mgr.RemoveWeight(this);

                Teleport(pickupSlot);
                mgr.SelectWeight(this);                // devient le poids actif
            }
        }

        /* ----- téléportation + feedback ----- */
        public void Teleport(Transform target)
        {
            transform.position = target.position;
            transform.position = new Vector3(target.position.x, target.position.y + height/2, target.position.z);
            transform.rotation = target.rotation;
        }

        // public void SetHighlight(bool on)
        // {
        //     rend.material.color = on ? Color.yellow : baseColor;
        // }
    }
}
