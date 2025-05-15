using UnityEngine;
using Attribute.Runtime;

namespace ScriptableObjectArchitecture.Runtime {
    public enum ItemType {
        special,
        consumable,
        displayable,
        usable,
        book
    }

    [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
    public class ItemData : ScriptableObject {
        public string itemName;
        [TextArea] public string description;

        [Header("Visuals")]
        public Sprite icon;
        public GameObject worldPrefab;
        [Header("Stats / Custom data")]
        public int  maxStack = 1;
        public ItemType type;
        [ShowIf("type", ItemType.displayable)] public GameObject uiPrefab;
        [ShowIf("type", ItemType.displayable)] public bool DisplayOnPickup = false;

        [ShowIf("type", ItemType.book)] public GameObject bookUIPrefab;
    }
}