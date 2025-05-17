using UnityEngine;
using System.Collections;

namespace Wheight.Runtime
{
    public class CylinderRotator : MonoBehaviour
    {
        [Tooltip("Référence au cylindre à faire pivoter")]
        public Transform cylinder;

        // private const float FaceAngle = 360f / 5f;   // 72°

        public float[] faceAngles = { -30f, -90f, -145f, -196f, -265f, -322f };
        public float duration = 0.4f;
        Quaternion baseRotation;

        Coroutine current;

        void Start()
        {
            baseRotation = transform.localRotation;
        }

        public void RotateToFace(int index)
        {
            if (index < 0 || index >= faceAngles.Length) return;

            Quaternion target = Quaternion.Euler(0f, faceAngles[index], 0f);

            if (duration <= 0f)
            {
                transform.localRotation = target;
                return;
            }

            if (current != null) StopCoroutine(current);
            current = StartCoroutine(RotateRoutine(target));
        }

        IEnumerator RotateRoutine(Quaternion target)
        {
            Quaternion start = transform.localRotation;
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime / duration;
                transform.localRotation = Quaternion.Slerp(start, target, t);
                yield return null;
            }
            transform.localRotation = target;
            current = null;
        }
    }
}
