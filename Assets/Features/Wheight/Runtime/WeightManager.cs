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

        [Header("Mesure")]
        public int initialWeight = 50;
        public int[] goodMeasurements = { 20, 40, 60, 90 };

        // État interne
        private List<WeightSelectable> leftWeights  = new();
        private List<WeightSelectable> rightWeights = new();
        public  WeightSelectable SelectedWeight { get; private set; }

        void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        /* ---------- Sélection ---------- */

        public void SelectWeight(WeightSelectable w)
        {
            // déselectionne l’ancien
            if (SelectedWeight != null)
                SelectedWeight.SetHighlight(false);

            SelectedWeight = w;
            if (w != null)
                w.SetHighlight(true);
        }

        /* ---------- Placement ---------- */

        public enum Side { Left, Right }

        public void PlaceSelectedOn(Side side)
        {
            if (SelectedWeight == null) return;

            // retire d'abord de l'autre plateau si besoin
            RemoveWeight(SelectedWeight, silent:true);

            if (side == Side.Left)
            {
                int idx = leftWeights.Count;
                if (idx >= leftSlots.Count) return;          // plateau plein
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
            SelectWeight(null);      // on laisse le poids posé, aucun poids sélectionné
        }

        public void RemoveWeight(WeightSelectable w, bool silent = false)
        {
            if (leftWeights.Remove(w) || rightWeights.Remove(w))
            {
                if (!silent) UpdateMeasure();
            }
        }

        private void UpdateMeasure()
        {
            int sumL = leftWeights.Sum(w => w.weightValue);
            int sumR = rightWeights.Sum(w => w.weightValue);
            int diff = sumL - sumR;
            int measured = initialWeight + diff;

            Debug.Log($"G:{sumL} | D:{sumR} | Δ:{diff} | Mesure :{measured}");

            if (goodMeasurements.Contains(measured))
                Debug.Log($" Bonne mesure : {measured}");
        }
    }
}
