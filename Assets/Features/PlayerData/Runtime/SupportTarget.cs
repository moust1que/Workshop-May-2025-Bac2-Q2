using UnityEngine;
using ScriptableObjectArchitecture.Runtime;

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

            // vérifie si cet Item est accepté
            if (System.Array.IndexOf(acceptedItems, sel) == -1) return;

            // instancie le prefab du monde
            Vector3 pos = (pivot ? pivot.position : transform.position);
            Quaternion rot = pivot ? pivot.rotation : transform.rotation;
            Instantiate(sel.worldPrefab, pos, rot);

            // consomme l'item dans l'inventaire visuel
            inv.ConsumeSelected();

            // optionnel : retirer de PlayerData
            Inventory.Instance?.RemoveOne(sel);

            occupied = true;   // empêche un second placement
        }
    }
}