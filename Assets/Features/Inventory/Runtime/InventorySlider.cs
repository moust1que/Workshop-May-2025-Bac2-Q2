using UnityEngine;
using UnityEngine.UIElements;
using GameManager.Runtime;

namespace Inventory.Runtime {
    using BBehaviour.Runtime;

    public class InventorySlider : BBehaviour {
        [SerializeField] UIDocument uiDoc;

        const float Width = 220f;
        VisualElement wrapper;
        Button toggle;
        bool isOpen;

        void Start() {
            wrapper = uiDoc.rootVisualElement.Q<VisualElement>("InventoryWrapper");
            toggle  = wrapper.Q<Button>("BtnToggle"); // si le bouton est à l’intérieur
            if (toggle != null) toggle.clicked += Toggle;
            else Debug.LogWarning("BtnToggle introuvable");

            // démarrer fermé
            wrapper.style.right = -Width;
        }

        private void OnEnable() {
            UIEvents.OnInventoryToggle += Toggle;
        }

        private void OnDisable() {
            UIEvents.OnInventoryToggle -= Toggle;
        }

        public void Toggle() {
            isOpen = !isOpen;
            wrapper = uiDoc.rootVisualElement.Q<VisualElement>("InventoryWrapper");
            wrapper.style.right = isOpen ? 0 : -Width;

            // (facultatif) faire pivoter le chevron
            if (toggle != null)
                toggle.style.rotate = new StyleRotate(new Rotate(isOpen ? 180 : 0));
        }
    }
}
