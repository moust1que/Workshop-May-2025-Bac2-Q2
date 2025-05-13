using UnityEngine;

namespace ScriptableObjectArchitecture.Runtime
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        [TextArea]   public string description;
        public bool   isSpecial;

        [Header("Visuals")]
        public Sprite icon;
        public GameObject worldPrefab;
        [Header("Stats / Custom data")]
        public int  maxStack = 1;
        public bool consumable;
    }
}
