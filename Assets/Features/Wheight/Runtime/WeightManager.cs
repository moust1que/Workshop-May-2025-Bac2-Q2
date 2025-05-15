using System.Linq;
using System.Collections.Generic;
using UnityEngine;


namespace Wheight.Runtime {
    using BBehaviour.Runtime;
    public class WeightManager : MonoBehaviour
    {
        public static WeightManager Instance { get; private set; }

        [Header("Plateaux")]
        public Transform leftPan;
        public Transform rightPan;

        [Header("Emplacements (slots)")]
        public List<Transform> leftSlots;
        public List<Transform> rightSlots;

        [Header("Configuration")]
        [Tooltip("Distance max pour snap")]
        public float snapThreshold = 1f;
        [Tooltip("Poids initial au centre")]
        public int initialWeight = 50;

        private List<WeightDragDrop> leftWeights = new List<WeightDragDrop>();
        private List<WeightDragDrop> rightWeights = new List<WeightDragDrop>();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public enum Side { Left, Right }

        public void TryPlaceWeight(WeightDragDrop weight)
        {
            float dLeft = Vector3.Distance(weight.transform.position, leftPan.position);
            float dRight = Vector3.Distance(weight.transform.position, rightPan.position);

            if (dLeft <= snapThreshold) PlaceWeight(weight, Side.Left);
            else if (dRight <= snapThreshold) PlaceWeight(weight, Side.Right);
            else RemoveWeight(weight);
        }
        
        public void PlaceWeight(WeightDragDrop weight, Side side)
        {
            RemoveWeight(weight);

            if (side == Side.Left)
            {
                leftWeights.Add(weight);
                int idx = leftWeights.Count - 1;
                weight.transform.position = leftSlots[idx].position;
            }
            else
            {
                rightWeights.Add(weight);
                int idx = rightWeights.Count - 1;
                weight.transform.position = rightSlots[idx].position;
            }

            UpdateMeasurement();
        }

        public void RemoveWeight(WeightDragDrop weight)
        {
            if (leftWeights.Remove(weight) || rightWeights.Remove(weight))
                UpdateMeasurement();
            weight.ResetToInitialPosition();
        }

        private void UpdateMeasurement()
        {
            int sumL = leftWeights.Sum(w => w.weightValue);
            int sumR = rightWeights.Sum(w => w.weightValue);
            int diff = sumL - sumR;                   
            int measured = initialWeight + diff;           

            Debug.Log($"Gauche : {sumL} | Droite : {sumR} | Différence : {diff} | Mesure : {measured}");

            int[] good = { 20, 40, 60, 90 };
            if (good.Contains(measured))
                Debug.Log($"✅ Bonne mesure : {measured}");
        }
    }
}
