using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Wheight.Runtime {
    using BBehaviour.Runtime;
    public class WeightManager : MonoBehaviour
    {
        public static WeightManager Instance { get; private set; }

        public List<Transform> leftSlots;
        public List<Transform> rightSlots;

        public int initialWeight = 50;
        public int[] goodMeasurements = { 20, 40, 60, 90 };

        public Transform needle;
        public float minAngle = -35f;
        public float midAngle = 0f;
        public float maxAngle = +35f;
        public int minValue = 20;
        public int maxValue = 90;

        private List<WeightSelectable> leftWeights = new();
        private List<WeightSelectable> rightWeights = new();
        public WeightSelectable SelectedWeight { get; private set; }

        void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void SelectWeight(WeightSelectable w)
        {
            if (SelectedWeight != null)
                SelectedWeight.SetHighlight(false);

            SelectedWeight = w;
            if (w != null)
                w.SetHighlight(true);
        }

        public enum Side { Left, Right }

        public void PlaceSelectedOn(Side side)
        {
            if (SelectedWeight == null) return;

            RemoveWeight(SelectedWeight, silent: true);

            if (side == Side.Left)
            {
                int idx = leftWeights.Count;
                if (idx >= leftSlots.Count) return;
                leftWeights.Add(SelectedWeight);
                SelectedWeight.Teleport(leftSlots[idx]);
            }
            else
            {
                int idx = rightWeights.Count;
                if (idx >= rightSlots.Count) return;
                rightWeights.Add(SelectedWeight);
                SelectedWeight.Teleport(rightSlots[idx]);
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
        {
            return leftWeights.Contains(w) || rightWeights.Contains(w);
        }

        private void UpdateMeasure()
        {
            int sumL = leftWeights.Sum(w => w.weightValue);
            int sumR = rightWeights.Sum(w => w.weightValue);
            int measured = initialWeight + (sumR - sumL);

            int clamped = Mathf.Clamp(measured, minValue, maxValue);
            float t = (clamped - minValue) / (float)(maxValue - minValue);
            float angle = Mathf.Lerp(minAngle, maxAngle, t);
            needle.localRotation = Quaternion.Euler(0f, 0f, angle);

            Debug.Log($"G:{sumL} | D:{sumR}| Mesure :{measured}");

            if (goodMeasurements.Contains(measured))
                Debug.Log($" Bonne mesure : {measured}");
        }
    }
}

