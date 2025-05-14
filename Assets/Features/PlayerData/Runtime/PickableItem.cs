using UnityEngine;
using ScriptableObjectArchitecture.Runtime;


namespace PlayerData.Runtime
{
    public class PickableItem : MonoBehaviour
    {
        public Sprite icon;
        public Inventory playerData;
        public ItemData type;
        public bool isPickable = true;

        void OnMouseDown() {
            if (!isPickable) return;
            playerData.Add(type);

            var inv = FindFirstObjectByType<InventoryCanvasManager>();
            if (inv) inv.AddItem(type);  

            Destroy(gameObject);
        }
    }
}
