using UnityEngine;
using System.Collections.Generic;

namespace ScriptableObjectArchitecture.Runtime
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
    public class Inventory : ScriptableObject
    {
        public class ItemEntry
        {
            public ItemData type;
            public int count = 1;
        }

        public List<ItemEntry> inventory = new();

        /* API : ajouter un objet */
        public void Add(ItemData type)
        {
            var entry = inventory.Find(e => e.type == type && e.count < type.maxStack);
            if (entry != null) entry.count++;
            else               inventory.Add(new ItemEntry { type = type, count = 1 });
        }

        public void Clear() => inventory.Clear();
        
    }
}
