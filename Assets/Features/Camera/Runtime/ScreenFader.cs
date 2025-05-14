using UnityEngine;
using UnityEngine.UI;

namespace CameraManager.Runtime {
    public class ScreenFader : MonoBehaviour {
        public Image fadeImage;
        public float defaultDuration = 1f;

        bool  _isFading;
        float _from, _to; 
        float _timer, _dur;

        void Awake() {
            if (fadeImage == null) {
                var canvasGO = new GameObject("FadeCanvas", typeof(Canvas));
                var canvas   = canvasGO.GetComponent<Canvas>();
                canvas.renderMode  = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = short.MaxValue;
                canvasGO.transform.SetParent(transform, false);

                var imgGO = new GameObject("FadeImage", typeof(Image));
                imgGO.transform.SetParent(canvasGO.transform, false);
                fadeImage = imgGO.GetComponent<Image>();
                fadeImage.color = Color.black;

                var rt = fadeImage.rectTransform;
                rt.anchorMin = Vector2.zero; rt.anchorMax = Vector2.one;
                rt.offsetMin = rt.offsetMax = Vector2.zero;
            }

            fadeImage.raycastTarget = false;
        }

        void Update() {
            if (!_isFading) return;

            _timer += Time.deltaTime;
            float t = Mathf.Clamp01(_timer / _dur); 
            SetAlpha(Mathf.Lerp(_from, _to, t));

            if (t >= 1f) _isFading = false;     
        }


        public void FadeOut(float duration = -1f) => StartFade(GetAlpha(), 1f, duration);
        public void FadeIn (float duration = -1f) => StartFade(GetAlpha(), 0f, duration);

        public bool IsFading => _isFading;

        void StartFade(float from, float to, float duration) {
            _from     = from;
            _to       = to;
            _dur      = duration < 0 ? defaultDuration : duration;
            _timer    = 0f;
            _isFading = true;
        }

        float GetAlpha() => fadeImage.color.a;
        void  SetAlpha(float a) {
            var c = fadeImage.color;
            c.a = a;
            fadeImage.color = c;
        }
    }
}
