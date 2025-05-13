using System.Collections.Generic;
using ScriptableObjectArchitecture.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerData.Runtime
{
    public class InventoryCanvasManager : MonoBehaviour
    {
        public RectTransform panel;        // InventoryPanel
        public Button        toggleBtn;    // ToggleBtn
        public SlotUi[]      specialSlots; // 3 slots du haut
        public Transform     gridParent;   // GridContainer
        public SlotUi        slotPrefab;   // Prefab Slot (96×64)

        [Header("Motion")]
        public float slideDist = 260f;
        public float speed     = 10f;

        readonly List<SlotUi> allSlots = new();
        bool isOpen;
        Vector2 closedPos, openPos;

        public ItemData SelectedItem { get; private set; }

        void Awake()
        {
            closedPos = panel.anchoredPosition;               // ( 230, 0 )
            openPos   = closedPos + Vector2.left * slideDist; // (   0, 0 )

            toggleBtn.onClick.AddListener(() => {
                isOpen = !isOpen;
                toggleBtn.transform.rotation = Quaternion.Euler(0, 0, isOpen ? 0f : 180f);
            });

            // enregistrer les 3 slots spéciaux pour le highlight
            allSlots.AddRange(specialSlots);

            // enregistrer les 12 slots initiaux de la grille
            foreach (Transform t in gridParent)
                allSlots.Add(t.GetComponent<SlotUi>());

            // ajouter le Listener de clic sur chacun
            foreach (SlotUi ui in allSlots)
                ui.GetComponent<Button>().onClick.AddListener(() => ClickSlot(ui));
        }

        /*============  API appelée par PickableItem  ============*/
        public void AddItem(ItemData type)
        {
            // 1) slots spéciaux déjà pleins ?
            var pool = type.isSpecial ? specialSlots : gridParent.GetComponentsInChildren<SlotUi>();

            foreach (SlotUi ui in pool)
            {
                if (ui.isEmpty) { ui.SetItem(type); return; }
            }

            // 2) instancier une nouvelle case normale si besoin
            if (!type.isSpecial)
            {
                SlotUi newSlot = Instantiate(slotPrefab, gridParent);
                newSlot.SetItem(type);
                allSlots.Add(newSlot);
                newSlot.GetComponent<Button>().onClick.AddListener(() => ClickSlot(newSlot));
            }
        }

        /*============  PRIVÉ  ============*/
        
        void ClickSlot(SlotUi uiSlot)
        {
            if (uiSlot.isEmpty) return;

            // highlight
            foreach (SlotUi s in allSlots) s.ClearHighlight();
            uiSlot.Highlight(true);

            // mémorise l'objet sélectionné
            SelectedItem = uiSlot.currentItem;
            selectedSlot = uiSlot;            // (stocke le Slot pour le vider plus tard)
        }
        SlotUi selectedSlot; 

        public void ConsumeSelected()
        {
            if (selectedSlot == null) return;
            selectedSlot.Clear();             // visuel
            SelectedItem = null;
            selectedSlot = null;
        }  

        void Update()
        {
            Vector2 target = isOpen ? openPos : closedPos;
            panel.anchoredPosition = Vector2.Lerp(panel.anchoredPosition, target, Time.deltaTime * speed);
        }
    }
}
