using UnityEngine;

namespace CameraManager.Runtime
{
    public class CameraShake : MonoBehaviour
    {
        [Header("Réglages")]
        public float magnitude = 0.1f; 
        public float duration  = 0.25f; 
        public Vector2 intervalRange = new(2,5);

        Vector3 _origin;
        float _shakeTimer;
        float _nextShakeTime;
        bool _isShaking;

        void Start() {
            _origin = transform.localPosition;
            _nextShakeTime = Time.time + Random.Range(intervalRange.x, intervalRange.y);
        }

        void Update() {
            if(!_isShaking) return;

            if(Time.time >= _nextShakeTime && _shakeTimer <= 0f) {
                _shakeTimer = duration;
                _nextShakeTime = Time.time + Random.Range(intervalRange.x, intervalRange.y);
            }

            if(_shakeTimer > 0f) {
                float offsetX = Random.Range(-magnitude, magnitude);  
                transform.localPosition = _origin + new Vector3(offsetX, 0f, 0f);

                _shakeTimer -= Time.deltaTime;
                if(_shakeTimer <= 0f) transform.localPosition = _origin;
            }
        }

        public bool isShaking {
            get { return _isShaking; }
            set { _isShaking = value; }
        }
    }
}

