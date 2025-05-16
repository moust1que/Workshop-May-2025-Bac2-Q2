using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Wheight.Runtime {
    using BBehaviour.Runtime;
    public class WeightManager : MonoBehaviour
    {
       /* ---------- Singleton ---------- */
        public static WeightManager Instance { get; private set; }

        /* ---------- Références scène ---------- */
        [Header("Slots de plateau")]
        public List<Transform> leftSlots;
        public List<Transform> rightSlots;

        [Header("Aiguille")]
        public Transform needle;               // pivot de l’aiguille
        public float minAngle = -35f;          // angle pour la valeur minValue
        public float maxAngle =  35f;          // angle pour la valeur maxValue
        public int   minValue = 20;            // borne basse de la jauge
        public int   maxValue = 90;            // borne haute  de la jauge

        [Header("Mesure")]
        public int   initialWeight    = 50;    // point milieu
        public int[] goodMeasurements = { 20, 40, 60, 70, 90 };

        [Header("Cylindre à tourner")]
        public CylinderRotator cylinderRotator;

        /* ---------- État interne ---------- */
        private readonly List<WeightSelectable> leftWeights  = new();
        private readonly List<WeightSelectable> rightWeights = new();
        public  WeightSelectable SelectedWeight { get; private set; }

        /* ---------- Cycle vie ---------- */
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        /* ---------- Sélection ---------- */
        public void SelectWeight(WeightSelectable weight)
        {
            // On se contente de mémoriser la sélection
            SelectedWeight = weight;
        }

        /* ---------- Placement / retrait ---------- */
        public enum Side { Left, Right }

        public void PlaceSelectedOn(Side side)
        {
            if (SelectedWeight == null) return;

            // le poids ne doit appartenir à aucun plateau avant placement
            RemoveWeight(SelectedWeight, silent: true);

            if (side == Side.Left)
            {
                if (leftWeights.Count >= leftSlots.Count) return;   // plateau plein
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
            SelectWeight(null);   // on libère la sélection
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

        /* ---------- Mesure & affichage ---------- */
        private void UpdateMeasure()
        {
            int sumL     = leftWeights.Sum(w => w.weightValue);
            int sumR     = rightWeights.Sum(w => w.weightValue);
            int measured = initialWeight + (sumR - sumL);           // droite +, gauche -

            /* --- aiguille --- */
            int   clamped = Mathf.Clamp(measured, minValue, maxValue);
            float t       = (clamped - minValue) / (float)(maxValue - minValue);
            float angle   = Mathf.Lerp(minAngle, maxAngle, t);
            needle.localRotation = Quaternion.Euler(-angle, 0f, 0f);

            /* --- cylindre (faces) --- */
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

