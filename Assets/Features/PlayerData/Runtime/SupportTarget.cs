using UnityEngine;
using ScriptableObjectArchitecture.Runtime;
using Events.Runtime;

namespace PlayerData.Runtime
{
    public class SupportTarget : MonoBehaviour
    {
        [Tooltip("Les items acceptés (drag les ScriptableObjects)")]
        public ItemData[] acceptedItems;

        [Tooltip("Position où instancier l'objet (laissez vide → au centre)")]
        public Transform pivot;

        bool occupied;

        void OnMouseDown() {
            if (occupied) return;

            var inv = FindFirstObjectByType<InventoryCanvasManager>();
            if (inv == null || inv.SelectedItem == null) return;

            ItemData sel = inv.SelectedItem;

            if (System.Array.IndexOf(acceptedItems, sel) == -1) return;

            Vector3 pos = (pivot ? pivot.position : transform.position);
            Quaternion rot = pivot ? pivot.rotation : transform.rotation;
            Instantiate(sel.worldPrefab, pos, rot);

            inv.ConsumeSelected();

            Inventory.Instance?.RemoveOne(sel);

            GameEvents.OnItemUsed?.Invoke(sel);

            occupied = true;  
        }
    }
}