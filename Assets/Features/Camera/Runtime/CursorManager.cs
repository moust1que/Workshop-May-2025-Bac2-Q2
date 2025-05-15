using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Attribute.Runtime;

namespace CameraManager.Runtime {
    using BBehaviour.Runtime;

    public class CursorManager : BBehaviour {
        [Header("Textures")]
        public Texture2D defaultCursor;
        public Texture2D hoverCursor;
        public Vector2   hotspot  = Vector2.zero;
        public CursorMode cursorMode = CursorMode.Auto;

        [Header("LayerMask des objets cliquables")]
        public LayerMask hoverMask = ~(1 << 3);

        private Camera cam;
        private bool isHovering;

        private GameObject hoverObject = null;
        private GameObject instantiatedObject = null;

        private float ShaderValue = 0f;
        private bool animateShader = false;

        private MaterialPropertyBlock propertyBlock;
        private Renderer instRenderer;
        [SerializeField] private Shader shader;

        void Awake() {
            cam = Camera.main;
            Cursor.SetCursor(defaultCursor, hotspot, cursorMode);
            propertyBlock = new MaterialPropertyBlock();
        }

        void Update() {
            bool hoverNow = false;
            GameObject detectedObject = null;

            if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) {
                PointerEventData pointer = new PointerEventData(EventSystem.current) {
                    position = Input.mousePosition
                };
                List<RaycastResult> results = new();
                EventSystem.current.RaycastAll(pointer, results);
                
                foreach(RaycastResult res in results) {
                    if(res.gameObject.CompareTag("Hoverable")) {
                        hoverNow = true;
                        break;
                    }
                }

                SetCursor(hoverNow);
                return;
            }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit, 100f, hoverMask)) {
                Verbose($"Raycast hit {hit.collider.name}", VerboseType.Log);
                if(hit.collider.CompareTag("Hoverable")) {
                    hoverNow = true;
                    detectedObject = hit.transform.gameObject;

                    //! Shader, PRopertyBlock, Parent object, Scale parent * 1.1f, float -1 -> 1
                }
            }

            if(hoverNow) {
                if(hoverObject != detectedObject) {
                    hoverObject = detectedObject;

                    if(instantiatedObject != null) {
                        Destroy(instantiatedObject);
                    }

                    ShaderValue = -1f;
                    instantiatedObject = Instantiate(hoverObject, hoverObject.transform.position, hoverObject.transform.rotation, hoverObject.transform);
                    instantiatedObject.GetComponent<BoxCollider>().enabled = false;
                    instantiatedObject.transform.localScale = hoverObject.transform.localScale * 1.05f;

                    instRenderer = instantiatedObject.GetComponent<Renderer>();
                    
                    animateShader = true;
                    AnimateShaderValue();
                }
            }else {
                if(isHovering) {
                    if(instantiatedObject != null) {
                        Destroy(instantiatedObject);
                        instantiatedObject = null;
                    }

                    hoverObject = null;
                    animateShader = false;
                }
            }

            SetCursor(hoverNow);
        }

        void SetCursor(bool hover) {
            if(hover == isHovering) return;
            isHovering = hover;
            Cursor.SetCursor(hover ? hoverCursor : defaultCursor, hotspot, cursorMode);
        }

        void AnimateShaderValue() {
            if(!animateShader || instRenderer == null) return;

            ShaderValue += 0.1f;
            Mathf.Clamp(ShaderValue, -1f, 1f);

            instRenderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetFloat("_ShaderValue", ShaderValue);
            instRenderer.SetPropertyBlock(propertyBlock);

            DelayManager.instance.Delay(0.1f, AnimateShaderValue);
        }
    }
}