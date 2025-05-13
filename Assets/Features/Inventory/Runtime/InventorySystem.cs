using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace Inventory.Runtime
{
    public class InventorySystem : MonoBehaviour
    {
        public static InventorySystem Instance { get; private set; }

        [SerializeField] UIDocument uiDoc;
        [SerializeField] VisualTreeAsset slotTemplate;

        private VisualElement grid, specialRow;
        private readonly List<VisualElement> allSlots = new();

        private void Awake()
        {
            if (Instance != null && Instance != this){ Destroy(gameObject); return; }
            Instance = this;
        }

        private void Start()
        {
            var root = uiDoc.rootVisualElement;
            grid       = root.Q<VisualElement>("Grid");
            specialRow = root.Q<VisualElement>("SpecialRow");
            foreach (var s in specialRow.Children()) {
            allSlots.Add(s);
        }
        }

    public void AddItem(ItemData data) {
            if (data.isSpecial) {
                // 1️⃣ cherche le premier slot spécial encore vide
                var slot = specialRow.Q<VisualElement>(null, "empty");
                if (slot == null) {
                    Debug.LogWarning("Plus de place dans les slots spéciaux !");
                    return;
                }

                // 2️⃣ enlève la classe 'empty' et met l’icône
                slot.RemoveFromClassList("empty");
                slot.Q<Image>("Icon").sprite = data.icon;   // le <ui:Image> est déjà présent dans le template
                slot.RegisterCallback<ClickEvent>(_ => Toggle(slot));

                allSlots.Add(slot);                         // le slot fait maintenant partie de la liste
            }
            else {
                // --- cas normal inchangé ---
                var slot = slotTemplate.CloneTree().Q<VisualElement>();
                slot.AddToClassList("slot");
                slot.Q<Image>("Icon").sprite = data.icon;
                slot.RegisterCallback<ClickEvent>(_ => Toggle(slot));
                grid.Add(slot);
                allSlots.Add(slot);
            }
        }


        private void Toggle(VisualElement slot)
        {
            bool active = slot.ClassListContains("active");
            foreach (var s in allSlots) s.RemoveFromClassList("active");
            if (!active) slot.AddToClassList("active");
        }
    }
}
