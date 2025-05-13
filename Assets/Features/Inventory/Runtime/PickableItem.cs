using UnityEngine;

namespace Inventory.Runtime
{
    [RequireComponent(typeof(Collider))]
    public class PickableItem : MonoBehaviour
    {
        public ItemData data;

        private void OnMouseDown() {
            if (data == null) { Debug.LogError($"{name} n'a pas de ItemData"); return; }
            InventorySystem.Instance.AddItem(data);
            Destroy(gameObject);
        }
    }
}
