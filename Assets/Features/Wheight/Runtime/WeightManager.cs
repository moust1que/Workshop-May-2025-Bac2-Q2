using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Wheight.Runtime {
    using BBehaviour.Runtime;
    public class WeightManager : MonoBehaviour
    {

        public static WeightManager Instance { get; private set; }
        [Header("Slots de plateau")]
        public List<Transform> leftSlots;
        public List<Transform> rightSlots;

        [Header("Aiguille")]
        public Transform needle;       
        public float minAngle = -35f;     
        public float maxAngle =  35f;      
        public int   minValue = 20;     
        public int   maxValue = 90;       

        [Header("Mesure")]
        public int   initialWeight    = 50;    // point milieu
        public int[] goodMeasurements = { 20, 40, 60, 70, 90 };

        [Header("Cylindre à tourner")]
        public CylinderRotator cylinderRotator;

        private readonly List<WeightSelectable> leftWeights  = new();
        private readonly List<WeightSelectable> rightWeights = new();
        public  WeightSelectable SelectedWeight { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void SelectWeight(WeightSelectable weight)
        {
            SelectedWeight = weight;
        }

        public enum Side { Left, Right }

        public void PlaceSelectedOn(Side side)
        {
            if (SelectedWeight == null) return;

            RemoveWeight(SelectedWeight, silent: true);

            if (side == Side.Left)
            {
                if (leftWeights.Count >= leftSlots.Count) return;  
                leftWeights.Add(SelectedWeight);
                SelectedWeight.Teleport(leftSlots[leftWeights.Count - 1]);
            }
            else
            {
                if (rightWeights.Count >= rightSlots.Count) return;
                rightWeights.Add(SelectedWeight);
                SelectedWeight.Teleport(rightSlots[rightWeights.Count - 1]);
            }

            UpdateMeasure();
            SelectWeight(null);  
        }

        public void RemoveWeight(WeightSelectable w, bool silent = false)
        {
            if (leftWeights.Remove(w) || rightWeights.Remove(w))
            {
                if (!silent) UpdateMeasure();
            }
        }

        public bool IsOnPan(WeightSelectable w)
            => leftWeights.Contains(w) || rightWeights.Contains(w);

        private void UpdateMeasure()
        {
            int sumL     = leftWeights.Sum(w => w.weightValue);
            int sumR     = rightWeights.Sum(w => w.weightValue);
            int measured = initialWeight + (sumR - sumL);       

            int   clamped = Mathf.Clamp(measured, minValue, maxValue);
            float t       = (clamped - minValue) / (float)(maxValue - minValue);
            float angle   = Mathf.Lerp(minAngle, maxAngle, t);
            needle.localRotation = Quaternion.Euler(-angle, 0f, 0f);

            int faceIndex = System.Array.IndexOf(goodMeasurements, measured);
            if (faceIndex >= 0)
            {
                cylinderRotator.RotateToFace(faceIndex);
                Debug.Log($"Bonne mesure : {measured}  |  Face #{faceIndex}");
            }

            Debug.Log($"G:{sumL} | D:{sumR} | Mesure : {measured}");
        }
    }
}

