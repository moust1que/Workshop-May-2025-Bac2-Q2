using UnityEngine;
using UnityEngine.UIElements;

namespace Inventory.Runtime
{
    public class InventorySlider : MonoBehaviour
    {
        [SerializeField] UIDocument uiDoc;

        const float Width = 220f;
        VisualElement wrapper;
        Button toggle;
        bool isOpen;

        void Start() {
            wrapper = uiDoc.rootVisualElement.Q<VisualElement>("InventoryWrapper");
            toggle  = wrapper.Q<Button>("BtnToggle");      // si le bouton est à l’intérieur
            if (toggle != null)  toggle.clicked += Toggle;
            else                 Debug.LogWarning("BtnToggle introuvable");

            // démarrer fermé
            wrapper.style.right = -Width;
        }

        void Toggle() {
            isOpen = !isOpen;
            wrapper.style.right = isOpen ? 0 : -Width;

            // (facultatif) faire pivoter le chevron
            if (toggle != null)
                toggle.style.rotate = new StyleRotate(new Rotate(isOpen ? 180 : 0));
        }
    }
}
