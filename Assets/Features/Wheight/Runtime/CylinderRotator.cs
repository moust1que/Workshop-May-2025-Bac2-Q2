using UnityEngine;

namespace Wheight.Runtime
{
    public class CylinderRotator : MonoBehaviour
    {
        [Tooltip("Référence au cylindre à faire pivoter")]
        public Transform cylinder;

        private const float FaceAngle = 360f / 5f;   // 72°

        /// <param name="faceIndex">Indice de 0 à 4</param>
        public void RotateToFace(int faceIndex)
        {
            float target = faceIndex * FaceAngle;
            cylinder.localRotation = Quaternion.Euler(0f, target, 0f);
        }
    }
}
