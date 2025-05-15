using UnityEngine;

namespace Wheight.Runtime
{
    public class PanSelector : MonoBehaviour
    {
        public WeightManager.Side side;   // assigner Left ou Right dans l’inspecteur

        void OnMouseDown()
        {
            WeightManager.Instance.PlaceSelectedOn(side);
        }
    }
}
