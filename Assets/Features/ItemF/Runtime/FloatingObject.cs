using UnityEngine;

namespace ItemF.Runtime
{
    public class FloatingObject : MonoBehaviour
    {
        [Header("Amplitudes")]
        public float verticalAmplitude = 0.5f;
        public float horizontalAmplitude = 0.1f;

        [Header("Speed and Timings")]
        public float cyclesPerSecond = 1f;
        public Vector2 waitRange = new Vector2(2f, 5f);
        public Vector2 floatRange = new Vector2(1f, 2f);

        Vector3 startPosition;
        float nextStartTime;
        float currentFloatDuration;
        float elapsedTime;
        public bool floatingActive;

        void Start() {
            startPosition = transform.position;
            nextStartTime = Time.time + Random.Range(waitRange.x, waitRange.y);
        }

        void Update() {
            if (floatingActive) {
                elapsedTime += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsedTime / currentFloatDuration);

                float Offsety = Mathf.Sin(progress * Mathf.PI) * verticalAmplitude;
                float Offsetx = Mathf.Sin(progress * Mathf.PI * 1.5f) * horizontalAmplitude;

                transform.position = startPosition + new Vector3(Offsetx, Offsety, 0f);

                if (elapsedTime >= currentFloatDuration) {
                    floatingActive = false;
                    transform.position = startPosition;
                    nextStartTime = Time.time + Random.Range(waitRange.x, waitRange.y);
                }
            }
            else if (Time.time >= nextStartTime) {
                floatingActive = true;
                currentFloatDuration = Random.Range(floatRange.x, floatRange.y);
                elapsedTime = 0f;
            }
        }
    }
}
