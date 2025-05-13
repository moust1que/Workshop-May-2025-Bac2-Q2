using UnityEngine;
using ScriptableObjectArchitecture.Runtime;


namespace PlayerData.Runtime
{
    public class PickableItem : MonoBehaviour
    {
        public Sprite icon;
        public Inventory playerData;
        public ItemData type;

        void OnMouseDown()
        {
            playerData.Add(type);

            var inv = FindFirstObjectByType<InventoryCanvas>();
            if (inv) inv.AddItem(type);  

            Destroy(gameObject);
        }
    }
}
