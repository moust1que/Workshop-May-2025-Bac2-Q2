using UnityEngine;
using ScriptableObjectArchitecture.Runtime;
using UnityEngine.EventSystems;
using Events.Runtime;

namespace PlayerData.Runtime {
    using BBehaviour.Runtime;

    public class PickableItem : BBehaviour {
        public Sprite icon;
        public Inventory playerData;
        public ItemData type;
        public bool isPickable = true;

        void OnMouseDown() {
            Verbose("OnMouseDown");
            if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
            if(!isPickable) return;

            playerData.Add(type);

            var inv = FindFirstObjectByType<InventoryCanvasManager>();
            if(inv) inv.AddItem(type);
            Verbose("Item picked up");

            GameEvents.OnItemPickedUp?.Invoke(type);

            Destroy(gameObject);
        }
    }
}