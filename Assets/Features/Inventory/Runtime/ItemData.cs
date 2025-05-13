using UnityEngine;

namespace Inventory.Runtime
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
        public bool isSpecial; 
    }
}
